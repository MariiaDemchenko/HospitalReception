import { Component, ElementRef, OnInit, ViewChild, ViewEncapsulation, Input } from '@angular/core';
import { } from 'dhtmlx-scheduler';
import 'dhtmlx-scheduler/codebase/ext/dhtmlxscheduler_editors.js';
import 'dhtmlx-scheduler/codebase/ext/dhtmlxscheduler_limit.js';
import 'dhtmlx-scheduler/codebase/ext/dhtmlxscheduler_collision.js';

// import { } from '@types/dhtmlxscheduler';
import { EventService } from '../services/event.service';
import { Alert } from 'selenium-webdriver';
import { ActivatedRoute } from '@angular/router';
import { Event } from '../models/event';
import { Patient } from '../shared/patients/patient.model';
import { PatientsService } from '../shared/patients/patients.service';
import { ConsultationHours } from '../models/consultation-hours';
import { AlertsService } from 'angular-alert-module';
import { DatePipe } from '@angular/common';
import { ClosestConsultation } from '../models/closest-consultation';

export interface IAlert {
    id: number;
    type: string;
    message: string;
}

@Component({
    encapsulation: ViewEncapsulation.None,
    // tslint:disable-next-line:component-selector
    selector: 'scheduler',
    styleUrls: ['scheduler.component.css'],
    templateUrl: 'scheduler.component.html'
})
export class SchedulerComponent implements OnInit {
    @Input()
    public alerts: Array<IAlert> = [];
    private backup: Array<IAlert>;

    @ViewChild('scheduler_here') schedulerContainer: ElementRef;
    doctorId: any;
    patients: Patient[];
    patient: Patient;
    consultationHours: ConsultationHours[];
    minEventDuration: number;

    // tslint:disable-next-line:max-line-length
    constructor(private eventService: EventService, private activatedRoute: ActivatedRoute, private patientsService: PatientsService) {
        this.alerts.push({
            id: 1,
            type: 'success',
            message: 'This is an success alert',
        }, {
                id: 2,
                type: 'info',
                message: 'This is an info alert',
            });
        this.backup = this.alerts.map((alert: IAlert) => Object.assign({}, alert));
    }

    public closeAlert(alert: IAlert) {
        const index: number = this.alerts.indexOf(alert);
        this.alerts.splice(index, 1);
    }

    public reset() {
        this.alerts = this.backup.map((alert: IAlert) => Object.assign({}, alert));
    }

    private serializeEvent(data: any, insert: boolean = false): Event {
        const result = {};

        for (const i in data) {
            if (i.charAt(0) === '$' || i.charAt(0) === '_') { continue; }
            if (insert && i === 'id') { continue; }
            if (data[i] instanceof Date) {
                result[i] = scheduler.templates.xml_format(data[i]);
            } else {
                result[i] = data[i];
            }
        }
        return result as Event;
    }

    addAppointment() {
        const event = new Event();
        const datePipe = new DatePipe('en-US');
        let closestConsultation = new ClosestConsultation();
        // tslint:disable-next-line:max-line-length
        this.patientsService.getClosestConsultation(this.doctorId, this.minEventDuration).subscribe((data: ClosestConsultation) => {
            closestConsultation = data;
            event.start_date = datePipe.transform(closestConsultation.StartDate, 'dd-MM-yyyy HH:mm');
            event.end_date = datePipe.transform(closestConsultation.EndDate, 'dd-MM-yyyy HH:mm');
            event.patientId = '1';
            const id = scheduler.addEvent(event);
            scheduler.showLightbox(id);
        });
    }

    ngOnInit() {
        const backup = this.backup;
        const Patients = [];
        this.minEventDuration = 15;

        this.patientsService.getAllPatients().subscribe((data: Patient[]) => {
            this.patients = data;
            this.patients.forEach(patient => {
                // tslint:disable-next-line:max-line-length
                Patients.push({
                    key: patient.Id,
                    label: patient.FirstName + ' ' + patient.LastName + ' ' + '<b style=\'color:#F08080\'>' + patient.Email + '</b>',
                    short: patient.FirstName + ' ' + patient.LastName,
                });
            });
        });

        const patientsService = this.patientsService;
        const alertsService = this.alerts;

        this.activatedRoute.params.subscribe(paramsId => {
            this.doctorId = paramsId.id;
            scheduler.resetLightbox();
            scheduler.clearAll();
            scheduler.unblockTime([0, 1, 2, 3, 4, 5, 6], [0, 24 * 60]);
            scheduler.config.xml_date = '%Y-%m-%d %H:%i';
            scheduler.config.time_step = this.minEventDuration;
            scheduler.config.details_on_create = true;
            scheduler.config.details_on_dblclick = true;

            patientsService.getConsultationHours(this.doctorId).subscribe((data: ConsultationHours[]) => {
                this.consultationHours = data;
                this.consultationHours.forEach(element => {
                    // tslint:disable-next-line:max-line-length
                    scheduler.blockTime(element.DayOfWeek, [8 * 60, element.StartHour * 60 + element.StartMinutes]);
                    scheduler.blockTime(element.DayOfWeek, [element.EndHour * 60 + element.EndMinutes, 21 * 60]);
                });
                for (let i = 0; i < 7; i++) {
                    let isBlocked = true;
                    this.consultationHours.forEach(element => {
                        if (element.DayOfWeek === i) {
                            isBlocked = false;
                        }
                    });
                    if (isBlocked) {
                        scheduler.blockTime(i, [0, 24 * 60]);
                    }
                }
                scheduler.config.first_hour = 8;
                scheduler.config.last_hour = 21;

                scheduler.config.lightbox.sections = [
                    { name: 'Description', height: 50, map_to: 'text', type: 'textarea', focus: true },
                    {
                        name: 'Patient', options: Patients, map_to: 'patientId', type: 'combo',
                        height: 30, filtering: true, image_path: 'assets/scripts/codebase/imgs/'
                    },
                    { name: 'time', height: 72, type: 'time', map_to: 'auto' }
                ];

                scheduler.init(this.schedulerContainer.nativeElement);
                this.patientsService.getAllAppointments(this.doctorId).subscribe(
                    (appointments) => {
                        scheduler.parse(appointments, 'json');
                    }
                );
            });
        });

        scheduler.attachEvent('onTemplatesReady', function () {
            scheduler.templates.event_text = function (start, end, ev) {
                const description = ev.text ? ' ' + ev.text : '';
                // tslint:disable-next-line:max-line-length
                return ('<b style=\'color:white\'>' + scheduler.templates.event_date(start) + '-' + scheduler.templates.event_date(end) + description + '</b>');
            };
            scheduler.templates.event_header = function (start, end, event) {
                const selectedPatient = Patients.filter(p => p.key === event.patientId)[0];
                const fullName = selectedPatient === undefined ? '' : selectedPatient.short;
                return fullName;
            };
            scheduler.templates.lightbox_header = function (start, end, ev) {
                const description = ev.text ? ' ' + ev.text : '';
                const selectedPatient = Patients.filter(p => p.key === ev.patientId)[0];
                // tslint:disable-next-line:max-line-length
                const fullName = selectedPatient === undefined ? '' : '  for <i style=\'color:#F08080\'>' + selectedPatient.short + '</i>';

                // tslint:disable-next-line:max-line-length
                return '<i>' + description + '</i>' + fullName + '  from ' + '<i>' + scheduler.templates.event_date(start) + '</i>' + '  to ' + '<i>' + scheduler.templates.event_date(end) + '</i>';
            };

            scheduler.templates.event_class = function (start, end, event) {
                if (start < new Date()) {
                    return 'employee_event';
                }
            };
        });

        scheduler.attachEvent('onClick', function (event_id, e) {
            scheduler.showLightbox(event_id);
            return false;
        });

        scheduler.attachEvent('onBeforeLightbox', function (id) {
            const event = scheduler.getEvent(id);
            event.text = event.text !== 'New event' && event.text !== '' ? event.text : 'Consultation';
            return true;
        });

        scheduler.attachEvent('onEventChanged', (id, ev) => {
            ev.doctorId = this.doctorId;
            this.patientsService.updateAppointment(this.serializeEvent(ev)).subscribe();
        });

        scheduler.attachEvent('onEventDeleted', (id) => {
            this.patientsService.removeAppointment(id).subscribe();
        });

        scheduler.attachEvent('onLimitViolation', function (id, event) {
            if (event.probe) { return false; }

            const allowedStart = scheduler.checkLimitViolation({
                start_date: event.start_date,
                end_date: scheduler.date.add(event.start_date, scheduler.config.time_step, 'minute'),
                // tslint:disable-next-line:max-line-length
                probe: true
            });

            const allowedEnd = scheduler.checkLimitViolation({
                start_date: scheduler.date.add(event.end_date, -scheduler.config.time_step, 'minute'),
                end_date: event.end_date,
                probe: true
            });
            return true;
        });

        scheduler.attachEvent('onEventAdded', (id, ev) => {
            ev.doctorId = this.doctorId;
            this.patientsService.insertAppointment(this.serializeEvent(ev, true))
                .subscribe((response: Event) => {
                    if (response.id !== id) {
                        scheduler.changeEventId(id, response.id);
                    }
                });
        });

        scheduler.attachEvent('onEventSave', function (id, ev, is_new) {
            const allowedStart = scheduler.checkLimitViolation({
                start_date: ev.start_date,
                end_date: scheduler.date.add(ev.start_date, scheduler.config.time_step, 'minute'),
                probe: true
            });

            const allowedEnd = scheduler.checkLimitViolation({
                start_date: scheduler.date.add(ev.end_date, -scheduler.config.time_step, 'minute'),
                end_date: ev.end_date,
                probe: true
            });

            if (!allowedStart || !allowedEnd) {
                dhtmlx.message({
                    title: 'Consultation time out of range',
                    type: 'alert-error',
                    text: 'Please specify the correct consultation time'
                });
                this.alerts = this.backup.map((alert: IAlert) => Object.assign({}, alert));
                return false;
            }

            const collisioncCount = scheduler.getEvents(ev.start_date, ev.end_date).length;
            if (collisioncCount > 1) {
                dhtmlx.message({
                    title: 'Time is reserved',
                    type: 'alert-error',
                    text: 'This consultation time was already assigned'
                });
            }
            return true;
        });
    }
}

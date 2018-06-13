import { Component, ElementRef, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import 'dhtmlx-scheduler';
import 'dhtmlx-scheduler/codebase/ext/dhtmlxscheduler_editors.js';
import 'dhtmlx-scheduler/codebase/ext/dhtmlxscheduler_limit.js';

import { } from '@types/dhtmlxscheduler';
import { EventService } from '../services/event.service';
import { Alert } from 'selenium-webdriver';
import { ActivatedRoute } from '@angular/router';
import { Event } from '../models/event';
import { Patient } from '../shared/patients/patient.model';
import { PatientsService } from '../shared/patients/patients.service';
import { ConsultationHours } from '../models/consultation-hours';
@Component({
    encapsulation: ViewEncapsulation.None,
    // tslint:disable-next-line:component-selector
    selector: 'scheduler',
    styleUrls: ['scheduler.component.css'],
    templateUrl: 'scheduler.component.html'
})

export class SchedulerComponent implements OnInit {
    @ViewChild('scheduler_here') schedulerContainer: ElementRef;
    doctorId: any;
    patients: Patient[];
    patient: Patient;
    consultationHours: ConsultationHours[];

    constructor(private eventService: EventService, private activatedRoute: ActivatedRoute, private patientsService: PatientsService) { }

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

    ngOnInit() {
        const holders = [];
        this.patientsService.getAllPatients().subscribe((data: Patient[]) => {
            this.patients = data;
            this.patients.forEach(patient => {
                holders.push({ key: patient.Id, label: patient.FirstName });
            });
        });

        const patientsService = this.patientsService;
        this.activatedRoute.params.subscribe(paramsId => {
            this.doctorId = paramsId.id;
            scheduler.unblockTime([0, 1, 2, 3, 4, 5, 6], [0, 24 * 60]);
            scheduler.config.xml_date = '%Y-%m-%d %H:%i';

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
                    { name: 'PatientFullName', height: 50, map_to: 'patientFullName', type: 'textarea', focus: true },
                    {
                        name: 'holders', options: holders, map_to: 'patientId', type: 'combo',
                        height: 30, filtering: true, image_path: 'assets/scripts/codebase/imgs/', onchange: function () {
                            const selectedPatientId = scheduler.formSection('holders').getValue();
                            patientsService.getPatientById(selectedPatientId).subscribe((patient: Patient) => {
                                scheduler.formSection('PatientFullName').setValue(patient.FirstName);
                            });
                        }
                    },
                    { name: 'time', height: 72, type: 'time', map_to: 'auto' }
                ];

                scheduler.init(this.schedulerContainer.nativeElement);

                this.eventService.get(this.doctorId)
                    .then((doctor) => {
                        scheduler.parse(doctor, 'json');
                    });
            });

            scheduler.attachEvent('onTemplatesReady', function () {
                scheduler.templates.event_header = function (start, end, ev) {
                    return ('# <b style=\'color:#F08080\'>' +
                        scheduler.templates.event_date(start) + ' - ' + scheduler.templates.event_date(end));
                };
                scheduler.templates.event_text = function (start, end, event) {
                    return '<b>' + event.text + '</b> </br>' + event.patientId + '</b> </br>' + event.patientFullName;
                };
            });

            scheduler.attachEvent('onEventAdded', (id, ev) => {
                ev.doctorId = this.doctorId;
                ev.patientId = 1;
                this.eventService.insert(this.serializeEvent(ev, true))
                    .then((response) => {
                        if (response.id !== id) {
                            scheduler.changeEventId(id, response.id);
                        }
                    });
            });

            scheduler.attachEvent('onEventChanged', (id, ev) => {
                ev.doctorId = this.doctorId;
                this.eventService.update(this.serializeEvent(ev));
            });

            scheduler.attachEvent('onEventDeleted', (id) => {
                this.eventService.remove(id);
            });


        });
    }
}

import { ChartPoint } from '../chart-point';
import { Measurement } from '../measurements';

export class Patient {
    Id: number;
    FirstName: string;
    LastName: string;
    MiddleName: string;
    Email: string;
    PhoneNumber: string;
    BirthDate: string;
    Gender: string;
    GenderId: string;
    Education: string;
    EducationId: string;
    DisabilityGroup: string;
    DisabilityGroupId: string;
    InformationSource: string;
    InformationSourceId: string;
    HabitationMember: string;
    HabitationMemberId: string;
    Policlinic: string;
    PoliclinicId: string;
    Organization: string;
    Employment: string;
    EmploymentId: string;
    LocalityType: string;
    LocalityTypeId: string;
    LocalityName: string;
    Region: string;
    RegionId: string;
    Measurements: Measurement[];
    SBPla: ChartPoint[];
    SBPra: ChartPoint[];
    SBPrl: ChartPoint[];
    SBPll: ChartPoint[];
    DBPla: ChartPoint[];
    DBPra: ChartPoint[];
}

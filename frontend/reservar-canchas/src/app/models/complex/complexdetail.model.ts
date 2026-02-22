import { ComplexState } from "./complexstate.enum";
import { ComplexServiceModel } from "./complexservice.model";
import { TimeSlotComplexModel } from "./timeslotcomplex.model";

export interface ComplexDetailModel{
  id:number;
  userId:number;
  name:string;
  description:string;
  province:string;
  locality:string;
  street:string;
  number:string;
  phone:string;
  cbu:string;
  imagePath:string;
  percentageSign:number;
  startIllumination:string;
  aditionalIllumination:number;
  cancelationReason?:string;
  complexState:ComplexState;
  services: ComplexServiceModel[];
  timeSlots: TimeSlotComplexModel[];
  averageRating:number;
}
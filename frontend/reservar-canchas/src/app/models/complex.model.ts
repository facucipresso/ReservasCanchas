import { ComplexState } from "./complexstate.enum";
import { ComplexServiceModel } from "./complexservice.model";
import { TimeSlotComplexModel } from "./timeslotcomplex.model";

export interface ComplexModel{
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
  startIlumination:string;
  aditionalIlumination:number;
  cancelationReason?:string;
  state:ComplexState;
  services: ComplexServiceModel[];
  timeSlots: TimeSlotComplexModel[];
  averageRating:number;
}
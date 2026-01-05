import { ComplexState } from "./complexstate.enum";
import { ComplexServiceModel } from "./complexservice.model";
import { TimeSlotComplexModel } from "./timeslotcomplex.model";
import { TimeSlotCreateModel } from "./timeslotscreate.model";

export interface ComplexCreateModel{
  userId:number;
  name:string;
  description:string;
  province:string;
  locality:string;
  street:string;
  number:string;
  phone:string;
  image:string;
  percentageSign:number;
  startIlumination:string;
  aditionalIlumination:number;
  cbu:number;
  servicesIds: number[];
  timeSlots: TimeSlotCreateModel[];
}
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
  image:File;
  percentageSign:number;
  startIlumination:string;
  aditionalIlumination:number;
  cbu:number;
  servicesIds: number[];
  timeSlots: TimeSlotCreateModel[];
}
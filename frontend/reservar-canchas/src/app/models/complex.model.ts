import { ComplexState } from "./complexstate.enum";
import { ServiceModel } from "./service.model";
import { TimeSlotComplexModel } from "./timeslotcomplex.model";

export interface ComplexModel{
  id:number;
  name:string;
  description:string;
  province:string;
  locality:string;
  street:string;
  number:string;
  phone:string;
  imagePath:string;
  percentageSign:number;
  startIlumination:string;
  aditionalIlumination:number;
  state:ComplexState;
  services: ServiceModel[];
  timeSlots: TimeSlotComplexModel[];
  averageRating:number;
}
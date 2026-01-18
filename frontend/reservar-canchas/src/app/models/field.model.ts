import { FieldState } from "./fieldstate.enum";
import { FieldType } from "./fieldtype.enum";
import { FloorType } from "./floortype.enum";
import { TimeSlotFieldModel } from "./timeslotfield.model";

export interface FieldModel{
  id:number;
  complexId:number;
  name:string;
  fieldType: keyof typeof FieldType; 
  floorType: keyof typeof FloorType; 
  fieldState:FieldState;
  hourPrice:number;
  ilumination:boolean;
  covered:boolean;
  active:boolean;
  timeSlotsField:TimeSlotFieldModel[];
}
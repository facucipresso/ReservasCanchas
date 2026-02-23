import { FieldState } from "./fieldstate.enum";
import { FieldType } from "./fieldtype.enum";
import { FloorType } from "./floortype.enum";
import { RecBlockResponseModel } from "../recblock/recblockresponse.model";
import { TimeSlotFieldModel } from "./timeslotfield.model";

export interface FieldDetailModel{
  id:number;
  complexId:number;
  name:string;
  fieldType: FieldType; 
  floorType: FloorType; 
  fieldState:FieldState;
  hourPrice:number;
  illumination:boolean;
  covered:boolean;
  active:boolean;
  timeSlotsField:TimeSlotFieldModel[];
  recurringCourtBlocks:RecBlockResponseModel[];
}
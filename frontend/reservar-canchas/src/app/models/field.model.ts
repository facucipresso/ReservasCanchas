import { FieldType } from "./fieldtype.enum";
import { FloorType } from "./floortype.enum";

export interface FieldModel{
  id:number;
  complexId:number;
  name:string;
  fieldType:FieldType;
  floorType:FloorType;
  initTime:string;
  endTime: string;
  hourPrice:number;
  ilumination:boolean;
  covered:boolean;
  active:boolean;
}
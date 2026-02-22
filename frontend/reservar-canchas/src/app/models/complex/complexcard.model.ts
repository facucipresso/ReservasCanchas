import { ComplexState } from "./complexstate.enum";

export interface ComplexCardModel{
  id:number;
  userId:number;
  name: string;
  province:string;
  locality:string;
  street:string;
  number:string;
  complexState:ComplexState;
  imagePath:string;
  lowestPricePerField:number;
}
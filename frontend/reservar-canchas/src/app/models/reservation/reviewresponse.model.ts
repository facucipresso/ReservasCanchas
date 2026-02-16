export interface ReviewResponse{
  id:number;
  userId:number;
  reservationId:number;
  name:string;
  lastname:string;
  comment:string;
  score:number;
  creationDate:string;
}
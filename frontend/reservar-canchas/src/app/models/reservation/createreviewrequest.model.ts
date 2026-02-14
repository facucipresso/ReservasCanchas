export interface CreateReviewRequest{
  reservationId: number;
  comment?: string;
  score: number;
}
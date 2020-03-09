import { Vacation } from "./vacation";

export interface CreateRequest {
  title: string;
  startDate: string;
  endDate: string;
}

export interface CreateResponse extends Vacation {

}
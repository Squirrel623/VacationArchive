export interface Vacation {
  id: number;
  createdBy: number;
  title: string;
  startDate: string;
  endDate: string;
}

export interface GetAllResponse {
  vacations: Vacation[];
}
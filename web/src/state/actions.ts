import { VacationActivity } from "../../generated-types/api/vacation/activity/vacationActivity";
import {GetAllResponse as GetAllVacationsResponse} from '../../generated-types/api/vacation/getAll';
import {GetAllResponse as GetAllActivitiesResponse} from '../../generated-types/api/vacation/activity/getAll';

import {ThunkAction, ThunkDispatch} from 'redux-thunk';
import axios from 'axios';

import {State} from './index';
import { Vacation } from "../../generated-types/api/vacation/vacation";

export const REQUEST_VACATIONS = 'REQUEST_VACATIONS';
export const RECEIVE_VACATIONS = 'RECEIVE_VACATIONS';
export const REQUEST_ACTIVITIES = 'REQUEST_ACTIVITIES';
export const RECEIVE_ACTIVITIES = 'RECEIVE_ACTIVITIES';
export const SELECT_ACTIVITY = 'SELECT_ACTIVITY';
export const REQUEST_ACTIVITY_MEDIA = 'REQUEST_ACTIVITY_MEDIA';
export const RECEIVE_ACTIVITY_MEDIA = 'RECEIVE_ACTIVITY_MEDIA';

export type Actions =
  ReturnType<typeof requestActivities> |
  ReturnType<typeof receiveActivities> |
  ReturnType<typeof requestVacations> |
  ReturnType<typeof receiveVacations>;

type ThunkResult<R> = ThunkAction<R, State, undefined, Actions>;

export function requestVacations() {
  return {
    type: REQUEST_VACATIONS as typeof REQUEST_VACATIONS,
  };
}

export function receiveVacations(vacations: Vacation[]) {
  return {
    type: RECEIVE_VACATIONS as typeof RECEIVE_VACATIONS,
    items: vacations,
  };
}

export function fetchVacations(): ThunkResult<Promise<void>> {
  return (dispatch) => {
    dispatch(requestVacations());
    return axios.get<GetAllVacationsResponse>('/api/vacations')
      // TODO: Handle error case
      .then((response) => {
        dispatch(receiveVacations(response.data.vacations));
      });
  };
}

export function requestActivities(vacationId: number) {
  return {
    type: REQUEST_ACTIVITIES as typeof REQUEST_ACTIVITIES,
    id: vacationId,
  };
}

export function receiveActivities(vacationId: number, data: VacationActivity[]) {
  return {
    type: RECEIVE_ACTIVITIES as typeof RECEIVE_ACTIVITIES,
    id: vacationId,
    items: data,
  };
}

export function fetchActivities(vacationId: number): ThunkResult<Promise<void>> {
  return (dispatch) => {
    dispatch(requestActivities(vacationId));
    return axios.get<GetAllActivitiesResponse>(`/api/vacations/${vacationId}/activities`)
      // TODO: handle error case
      .then((response) => {
        dispatch(receiveActivities(vacationId, response.data.vacationActivities));
      });
  };
}

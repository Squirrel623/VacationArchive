import {createStore, applyMiddleware} from 'redux';
import thunkMiddleware from 'redux-thunk';
import rootReducer from './reducers';

import { Vacation } from "../../generated-types/api/vacation/vacation";
import { VacationActivity } from "../../generated-types/api/vacation/activity/vacationActivity";
import { VacationActivityMedia } from "../../generated-types/api/vacation/activity/media/vacationActivityMedia";

export interface State {
  activitiesByVacation: {
    [id: number]: {
      isFetching: boolean,
      items: number[];
    }
  };
  entities: {
    vacations: {[id: number]: Vacation},
    activities: {[id: number]: VacationActivity},
    media: {[id: number]: VacationActivityMedia},
  };
}

export const store = createStore(
  rootReducer,
  undefined,
  applyMiddleware(thunkMiddleware),
);

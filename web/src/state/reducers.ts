import {combineReducers} from 'redux';

import {State} from './index';
import {
  Actions,
  RECEIVE_VACATIONS,
  RECEIVE_ACTIVITIES,
  REQUEST_ACTIVITIES,
} from './actions';
import { Vacation } from '../../generated-types/api/vacation/vacation';
import { VacationActivity } from '../../generated-types/api/vacation/activity/vacationActivity';

function vacationStateFromResult(result: Vacation[]): State['entities']['vacations'] {

}

function vacationActivityStateFromResult(result: VacationActivity[]): State['entities']['activities'] {

}

const initialState: State = {
  activitiesByVacation: {},
  entities: {
    vacations: {},
    activities: {},
    media: {},
  },
}

function entities(state: State['entities'] = initialState.entities, action: Actions): State['entities'] {
  switch(action.type) {
    case RECEIVE_VACATIONS:
      return {
        ...state,
        vacations: vacationStateFromResult(action.items),
      };
    case RECEIVE_ACTIVITIES:
      return {
        ...state,
        activities: vacationActivityStateFromResult(action.items),
      };
    default:
      return state;
  }
}

function activitiesByVacation(state: State['activitiesByVacation'] = initialState.activitiesByVacation, action: Actions): State['activitiesByVacation'] {
  switch(action.type) {
    case RECEIVE_ACTIVITIES:
      return {
        ...state,
        [action.id]: {
          isFetching: false,
          items: action.items.map((activity) => activity.id),
        },
      };
    default:
      return state;
  }
}

const rootReducer = combineReducers<State>({
  entities,
  activitiesByVacation,
});

export default rootReducer;
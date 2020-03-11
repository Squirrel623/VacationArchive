import React, {FunctionComponent} from 'react';
import {Link} from 'react-router-dom';
import {Button} from 'react-bootstrap';
import {VacationActivity} from '../../../../generated-types/api/vacation/activity/vacationActivity';

export const ActivityItem: FunctionComponent<VacationActivity> = (propActivity: VacationActivity) => {
  return (
    <div className="vacation-item">
      <h4>{propActivity.title}</h4>
      <div className="sub">Start Date: {new Date(propActivity.date).toLocaleString()}</div>
    </div>
  );
}
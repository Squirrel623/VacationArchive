import React, {FunctionComponent} from 'react';
import {VacationActivity} from '../../../../generated-types/api/vacation/activity/vacationActivity';
import {ActivityMediaUploader} from '../media/add'

export const ActivityItem: FunctionComponent<VacationActivity> = (propActivity: VacationActivity) => {
  return (
    <div className="vacation-item">
      <h4>{propActivity.title}</h4>
      <div className="sub">Start Date: {new Date(propActivity.date).toLocaleString()}</div>
      <ActivityMediaUploader vacationId={propActivity.vacationId} activityId={propActivity.id}/>
    </div>
  );
}
import React, {FunctionComponent, useEffect, useState} from 'react';
import {VacationActivity} from '../../../../generated-types/api/vacation/activity/vacationActivity';
import {ActivityMediaList} from './media/activity-media-list';

import {ActivityMediaUploader} from '../media/add';
import { VacationActivityMedia } from '../../../../generated-types/api/vacation/activity/media/vacationActivityMedia';

export const ActivityItem: FunctionComponent<VacationActivity> = (propActivity: VacationActivity) => {
  const [newMediaItem, setNewMediaItem] = useState<VacationActivityMedia>();

  const onMediaUploaded = (newMedia: VacationActivityMedia) => {
    setNewMediaItem(newMedia);
    setNewMediaItem(undefined);
  }

  return (
    <div className="vacation-item">
      <h4>{propActivity.title}</h4>
      <div className="sub">Start Date: {new Date(propActivity.date).toLocaleString()}</div>
      <ActivityMediaList newMediaItem={newMediaItem} vacationId={propActivity.vacationId} activityId={propActivity.id}></ActivityMediaList>
      <ActivityMediaUploader onComplete={onMediaUploaded} vacationId={propActivity.vacationId} activityId={propActivity.id}/> 
    </div>
  );
}
import React, {FunctionComponent/* , useEffect, useState */} from 'react';
//import axios, {AxiosResponse} from 'axios';

//import {GetResponse} from '../../../../../generated-types/api/vacation/activity/media/get';

export interface ActivityMediaItemProps {
  id: number;
  activityId: number;
  vacationId: number;
  contentType: string;
}

export const ActivityMediaItem: FunctionComponent<ActivityMediaItemProps> = (props: ActivityMediaItemProps) => {
  function getContentWrapper() {
    const url = `/api/vacations/${props.vacationId}/activities/${props.activityId}/media/${props.id}/contents`;

    if (props.contentType.startsWith('image')) {
      return (
        <img alt="" src={url} height="auto" width="100%"></img>
      );
    } else {
      return (
        <video preload="metadata" height="auto" width="100%" controls>
          <source src={url} type={props.contentType}/>
          <p>Your browser does not support videos with content type "{props.contentType}"</p>
        </video>
      );
    }
  }

  return (
    <div>
      {getContentWrapper()}
    </div>
  );
}
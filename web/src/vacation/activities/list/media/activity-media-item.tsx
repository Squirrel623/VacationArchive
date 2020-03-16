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
  /* const [url, setUrl] = useState<string>();
  const [contentType, setContentType] = useState<string>();

  useEffect(() => {
    const fetch = async () => {
      try {
        const mediaRecord = await axios.get<any, AxiosResponse<GetResponse>>(`/api/vacations/${props.vacationId}/activities/${props.activityId}/media/${props.id}`);
        setContentType(mediaRecord.data.contentType);
        setUrl(`/api/vacations/${props.vacationId}/activities/${props.activityId}/media/${props.id}/contents`);
      } catch {
        setContentType(undefined);
        setUrl(undefined);
      }
    };

    fetch();
  }, [props.vacationId, props.activityId, props.id]); */

  function getContentWrapper() {
    const url = `/api/vacations/${props.vacationId}/activities/${props.activityId}/media/${props.id}/contents`;

    if (props.contentType.startsWith('image')) {
      return (
        <img alt="" src={url} height="auto" width="100%"></img>
      );
    } else {
      return (
        <video src={url}></video>
      );
    }
  }

  return (
    <div>
      {getContentWrapper()}
    </div>
  );
}
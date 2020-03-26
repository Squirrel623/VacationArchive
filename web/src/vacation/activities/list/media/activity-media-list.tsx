import React, {FunctionComponent, useState, useEffect} from 'react';
import axios, {AxiosResponse} from 'axios';
import {ListGroup} from 'react-bootstrap';

import {VacationActivityMedia} from '../../../../../generated-types/api/vacation/activity/media/vacationActivityMedia';
import {GetAllResponse} from '../../../../../generated-types/api/vacation/activity/media/getAll';

import {ActivityMediaItem} from './activity-media-item';

export interface ActivityMediaListProps {
  vacationId: number;
  activityId: number;
  newMediaItem?: VacationActivityMedia;
}

export const ActivityMediaList: FunctionComponent<ActivityMediaListProps> = (props: ActivityMediaListProps) => {
  const [mediaList, setMediaList] = useState<VacationActivityMedia[]>();

  useEffect(() => {
    if (!props.newMediaItem) {
      return;
    }

    if (!mediaList) {
      setMediaList([props.newMediaItem]);
      return;
    }

    mediaList.push(props.newMediaItem);
  }, [props.newMediaItem])

  useEffect(() => {
    const fetch = async () => {
      try {
        const response = await axios.get<any, AxiosResponse<GetAllResponse>>(`/api/vacations/${props.vacationId}/activities/${props.activityId}/media`);
        setMediaList(response.data.items);
      } catch {
        debugger;
      }
    };

    fetch();
  }, [props.vacationId, props.activityId]);

  if (!mediaList) {
    return <div>Loading...</div>
  }

  const mediaItems = mediaList.map((mediaRecord) => 
    <ListGroup.Item key={mediaRecord.activityId.toString() + ' ' + mediaRecord.id.toString()}>
      <ActivityMediaItem id={mediaRecord.id} activityId={mediaRecord.activityId} vacationId={mediaRecord.vacationId} contentType={mediaRecord.contentType}/>
    </ListGroup.Item>
  );

  return (
    <ListGroup as="ul">
      {mediaItems}
    </ListGroup>
  );
};

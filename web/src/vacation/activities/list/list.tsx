import React, {FunctionComponent} from 'react';
import {ListGroup} from 'react-bootstrap';
import {VacationActivity} from '../../../../generated-types/api/vacation/activity/vacationActivity';
import {ActivityItem} from './item';

interface ActivityListProps {
  activites: VacationActivity[];
}

export const ActivityList: FunctionComponent<ActivityListProps> = (props: ActivityListProps) => {
  const activityItems = props.activites.map((activity) =>
    <ListGroup.Item key={activity.id}>
      <ActivityItem {...activity}/>
    </ListGroup.Item>
  );

  return (
    <div className="activity-list">
      <h3>Activities</h3>
      <ListGroup as="ul">
        {activityItems}
      </ListGroup>
    </div>
  );
}
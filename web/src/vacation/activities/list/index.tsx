import React, {FunctionComponent, useState, useEffect} from 'react';
import axios from 'axios';
import {VacationActivity} from '../../../../generated-types/api/vacation/activity/vacationActivity';
import {GetAllResponse as GetAllActivitiesResponse} from '../../../../generated-types/api/vacation/activity/getAll';
import {ActivityList as ActivityListBase} from './list';

export interface ActivityListProps {
  vacationId: number;
}

export const ActivityList: FunctionComponent<ActivityListProps> = (props: ActivityListProps) => {
  const [activities, setActivities] = useState<VacationActivity[]>();
  const [error, setError] = useState(false);

  useEffect(() => {
    const fetchData = async () => {
      setError(false);

      try {
        const activitiesResult = await axios.get<GetAllActivitiesResponse>(`/api/vacations/${props.vacationId}/activities`);
        if (activitiesResult.status !== 200) {
          debugger;
          setError(true);
          return;
        }
        setActivities(activitiesResult.data.vacationActivities);
      } catch(error) {
        setError(true);
      }
    };

    fetchData();
  }, [props.vacationId]);

  if (error) {
    return (
      <div>Error</div>
    );
  }

  if (!activities) {
    return (
      <div>Loading...</div>
    );
  }

  return (
    <div>
      <ActivityListBase activites={activities}/>
    </div>
  );
}
import React, {FunctionComponent, useEffect, useState} from 'react';
import axios from 'axios';
import { Vacation } from '../../../generated-types/api/vacation/vacation';
import { GetResponse } from '../../../generated-types/api/vacation/get';

interface ViewProps {
  id: string;
}

export const View: FunctionComponent<ViewProps> = (props: ViewProps) => {
  const [vacation, setVacationData] = useState<Vacation>();
  const [error, setError] = useState(false);

  useEffect(() => {
    const fetchData = async () => {
      setError(false);

      try {
        const result = await axios.get<GetResponse>(`/api/vacations/${props.id}`);
        if (result.status !== 200) {
          setError(true);
          return;
        }

        setVacationData(result.data);
      } catch(error) {
        setError(true);
      }
      
    };

    fetchData();
  }, [props.id]);

  if (error) {
    return <div>Error</div>;
  }

  if (!vacation) {
    return <div>Loading</div>;
  }

  return (
    <div>
      <h2>{vacation.title}</h2>
      <h4>Start Date: {new Date(vacation.startDate).toLocaleString()}</h4>
      <h4>End Date: {new Date(vacation.endDate).toLocaleString()}</h4>
    </div>
  );
}
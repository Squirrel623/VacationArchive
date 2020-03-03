import React, {useState, useEffect, FunctionComponent} from 'react';
import axios from 'axios';
import { Vacation, GetAllResponse } from '../../generated-types/api/vacation/getAll';
import {VacationList} from './vacation-list';

export const Home: FunctionComponent = () => {
  const [vacations, setVacations] = useState<Vacation[]>([]);
  useEffect(() => {
    const fetchData = async () => {
      const result = await axios.get<GetAllResponse>('/api/Vacation/GetAll');
      setVacations(result.data.vacations);
    }
    
    fetchData();
  }, []);

  return (
    <div>
      <VacationList vacations={vacations}/>
    </div>
  );
}
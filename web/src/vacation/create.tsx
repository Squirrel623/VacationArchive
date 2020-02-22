import React, {useState} from 'react';
import axios from 'axios';

import {AddRequest} from '../../generated-types/api/vacation/add';

function CreateVacation() {
  const todayString = new Date().toISOString().substring(0, 10);
  const [title, setTitle] = useState('');
  const [startDate, setStartDate] = useState(todayString);
  const [endDate, setEndDate] = useState(todayString);

  const submit = (event: React.FormEvent<HTMLFormElement>) => {
    const loginData: AddRequest = {
      title,
      startDate,
      endDate,
    };
    axios.post('/api/Vacation/Add', loginData);
    event.preventDefault();
  };

  return (
    <div>
      <h2>Create New Vacation</h2>
      <form onSubmit={submit}>
        <div>
          <label>
            Title:
            <input type="text" value={title} onChange={(event) => setTitle(event.target.value)}/>
          </label>
        </div>
        <div>
          <label>
            Start Date:
            <input type="date" value={startDate} onChange={(event) => setStartDate(event.target.value)}/>
          </label>
        </div>
        <div>
          <label>
            End Date:
            <input type="date" value={endDate} onChange={(event) => setEndDate(event.target.value)}/>
          </label>
        </div>
        <input type="submit" value="Submit"/>
      </form>
    </div>
  );
}

export {CreateVacation};
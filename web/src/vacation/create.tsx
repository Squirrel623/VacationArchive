import React, {useState} from 'react';
import axios, {AxiosResponse} from 'axios';

import {CreateRequest, CreateResponse} from '../../generated-types/api/vacation/create';

function CreateVacation() {
  const todayString = new Date().toISOString().substring(0, 10);
  const [title, setTitle] = useState('');
  const [startDate, setStartDate] = useState(todayString);
  const [endDate, setEndDate] = useState(todayString);

  const submit = (event: React.FormEvent<HTMLFormElement>) => {
    const loginData: CreateRequest = {
      title,
      startDate,
      endDate,
    };

    const fetch = async () => {
      try {
        const result = await axios.post<CreateRequest, AxiosResponse<CreateResponse>>('/api/vacations', loginData);
        if (result.status === 201) {
          console.log(result.data);
          // Redirect to new vacation
        } else {
          // ???
        }
      } catch (error) {

      }
    };

    fetch();
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
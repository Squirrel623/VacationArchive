import React, {useState, useEffect, FunctionComponent} from 'react';
import {Link, useRouteMatch, Switch, Route, useLocation} from 'react-router-dom';
import {Button} from 'react-bootstrap';
import axios from 'axios';
import {Vacation} from '../../generated-types/api/vacation/vacation';
import { GetAllResponse } from '../../generated-types/api/vacation/getAll';
import {VacationList} from './list/vacation-list';
import {CreateVacation} from './create';
import {View} from './view';

export const List: FunctionComponent = () => {
  const [vacations, setVacations] = useState<Vacation[]>([]);
  const location = useLocation();
  const {path, url} = useRouteMatch();

  useEffect(() => {
    if (location.pathname !== path) {
      return;
    }

    const fetchData = async () => {
      const result = await axios.get<GetAllResponse>('/api/vacations');
      setVacations(result.data.vacations);
    }
    
    fetchData();
  }, [location]);

  return (
    <div>
      <Switch>

        <Route exact path={path}>
          <VacationList vacations={vacations}/>
          <Link to={`${url}/create`}>
            <Button>Create</Button>
          </Link>
        </Route>

        <Route path={`${path}/view/:id`} render={(props) => <View id={props.match.params.id}/> }/>

        <Route path={`${path}/create`} component={CreateVacation}/>

      </Switch>
    </div>
  );
}
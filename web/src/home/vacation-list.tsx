import React, {FunctionComponent} from 'react';
import {ListGroup} from 'react-bootstrap';
import {VacationItem} from './vacation-item';
import { Vacation } from '../../generated-types/api/vacation/getAll';

interface VacationListProps {
  vacations: Vacation[];
}

export const VacationList: FunctionComponent<VacationListProps> = (props: VacationListProps) => {
  const vacationItems = props.vacations.map((vacation) =>
    <ListGroup.Item key={vacation.id}>
      <VacationItem title={vacation.title} startDate={vacation.startDate} endDate={vacation.endDate}/>
    </ListGroup.Item>
  );

  return (
    <ListGroup as="ul">
      {vacationItems}
    </ListGroup>
  );
}
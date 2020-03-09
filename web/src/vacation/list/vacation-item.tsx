import React, {FunctionComponent} from 'react';
import {Link} from 'react-router-dom';
import {Button} from 'react-bootstrap'
import './vacation-item.css';

interface VacationProps {
  id: number;
  title: string;
  startDate: string;
  endDate: string;
};

export const VacationItem: FunctionComponent<VacationProps> = (props: VacationProps) => {
  return (
    <div className="vacation-item">
      <h4>{props.title}</h4>
      <div className="sub">Start Date: {new Date(props.startDate).toLocaleString()}</div>
      <div className="sub">End Date: {new Date(props.endDate).toLocaleString()}</div>
      <Link to={`/vacations/view/${props.id}`}>
        <Button>View</Button>
      </Link>
    </div>
  );
}
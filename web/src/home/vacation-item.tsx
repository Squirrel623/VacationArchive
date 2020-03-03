import React, {FunctionComponent} from 'react';
import './vacation-item.css';

interface VacationProps {
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
    </div>
  );
}
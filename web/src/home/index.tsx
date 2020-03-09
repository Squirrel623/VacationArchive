import React, {useState, useEffect, FunctionComponent} from 'react';
import {Link} from 'react-router-dom';
import {Button} from 'react-bootstrap';

export const Home: FunctionComponent = () => {
  return (
    <div>
      <h2>Welcome to the Vacation Archive!</h2>
      <Link to="/vacations">
        <Button>Vacations</Button>
      </Link>
    </div>
  );
}
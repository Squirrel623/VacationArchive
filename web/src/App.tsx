import React from 'react';
import {Provider} from 'react-redux';
import {BrowserRouter as Router, Switch, Route} from 'react-router-dom';
import Navbar from 'react-bootstrap/Navbar';
import {Home} from './home';
import {List} from './vacation';

import {store} from './state';
import './App.css';

function App() {
  return (
    <Provider store={store}>
      <Router>
        <div className="App">
          <Navbar>
            <Navbar.Brand>Vacation Archive</Navbar.Brand>
          </Navbar>
          <Switch>
            <Route exact path="/">
              <Home/>
            </Route>
            <Route path="/vacations">
              <List/>
            </Route>
          </Switch>
        </div>
      </Router>
    </Provider>
  );
}

export default App;

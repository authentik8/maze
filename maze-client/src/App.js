import React, { Component } from 'react';
import {
    BrowserRouter as Router,
    Route
} from 'react-router-dom'
import './App.css';

import Home from './routes/Home'
import Maze from './routes/Maze'

class App extends Component {
  render() {
    return (
      <div className="App">
        <div className="App-header">
          <h2>Maze Solver</h2>
        </div>
        <Router>
          <div>
            <Route exact path="/" component={Home} />
            <Route path="/:width/:height" component={Maze} />
          </div>
        </Router>
      </div>
    );
  }
}

export default App;

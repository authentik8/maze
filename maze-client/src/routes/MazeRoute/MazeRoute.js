import React from 'react'
import 'whatwg-fetch'

import Maze from '../../components/Maze'
import './MazeRoute.css'

export class MazeRoute extends React.Component {

  refreshMaze = () => {
    this.refs.maze.getMaze()
  }

  render() {

    const { match: { params: { size } } } = this.props

    return (
      <div className='card mb-4'>
        <div className='card-header'>
          <h4 className='maze-card-title card-title mb-0 text-center'>
            <div className='maze-card-refresh-icon' onClick={this.refreshMaze}>
              <i className="fa fa-refresh" aria-hidden="true"></i>
            </div>
            <span>{size} x {size}</span>
          </h4>
        </div>
        <div className='card-body'>
          <Maze ref='maze' {...{ size } } />
        </div>
      </div>
    )
  }
}

export default MazeRoute

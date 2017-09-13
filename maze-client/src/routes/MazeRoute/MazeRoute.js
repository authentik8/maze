import React from 'react'
import 'whatwg-fetch'

import Maze from '../../components/Maze'

export class MazeRoute extends React.Component {

  render() {

    const { match: { params: { width, height } } } = this.props

    return (
      <div className='card'>
        <div className='card-header'>
          <h4 className='card-title mb-0 text-center'>{width} x {height}</h4>
        </div>
        <div className='card-body'>
          <Maze {...{ width, height } } />
        </div>
      </div>
    )
  }
}

export default MazeRoute

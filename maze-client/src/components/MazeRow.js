import React from 'react'

import MazeCell from './MazeCell'

export class MazeRow extends React.Component {

  shouldComponentUpdate(nextProps) {

    return ((!this.props.solution && nextProps.solution) || this.props.path.length !== nextProps.path.length)
  }

  render() {
    const { cells, path, solution, onClick } = this.props
    const cellContent = cells.map((cell, index) => (<MazeCell key={index} {...{ cell, path, solution, onClick } } />))
    return (
      <tr>
        {cellContent}
      </tr>
    )
  }
}

export default MazeRow

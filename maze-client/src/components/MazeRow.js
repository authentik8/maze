import React from 'react'

import MazeCell from './MazeCell'

export const MazeRow = ({ cells, path }) => {
  const cellContent = cells.map((cell, index) => (<MazeCell key={index} {...{ cell, path } } />))
  return (
    <tr>
      {cellContent}
    </tr>
  )
}

export default MazeRow

import React from 'react'

import MazeCell from './MazeCell'

export const MazeRow = ({ cells }) => {
  const cellContent = cells.map((cell, index) => (<MazeCell key={index} {...{ cell } } />))
  return (
    <tr>
      {cellContent}
    </tr>
  )
}

export default MazeRow

import React from 'react'

import MazeCell from './MazeCell'

export const MazeRow = ({ cells, path, solution, onClick }) => {
  const cellContent = cells.map((cell, index) => (<MazeCell key={index} {...{ cell, path, solution, onClick } } />))
  return (
    <tr>
      {cellContent}
    </tr>
  )
}

export default MazeRow

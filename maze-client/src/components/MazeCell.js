import React from 'react'

export const MazeCell = ({ cell }) => {
  const style = {}
  if (!cell.left) {
    style.borderLeft = 'solid 2px black'
  }
  if (!cell.up) {
    style.borderTop = 'solid 2px black'
  }
  if (!cell.right) {
    style.borderRight = 'solid 2px black'
  }
  if (!cell.down) {
    style.borderBottom = 'solid 2px black'
  }

  if (cell.goal) {
    style.backgroundColor = '#72db75'  
  }

  return (
    <td {...{ style }} ></td>
  )
}

export default MazeCell

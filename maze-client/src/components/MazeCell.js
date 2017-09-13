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
    style.backgroundColor = '#CE3914'  
  } else if (cell.start) {
    style.backgroundColor = '#72DB75'
    style.borderLeft = '#72DB75'
  }

  return (
    <td {...{ style }} ></td>
  )
}

export default MazeCell

import React from 'react'

export const ActionBar = ({ handleAction }) => {

  const defaultClasses = 'col-12 col-md-3 col-lg-2 mb-3 mb-lg-0'

  const buttonConfigs = [
    { name: 'left', text: 'Left', classes: defaultClasses },
    { name: 'right', text: 'Right', classes: defaultClasses },
    { name: 'up', text: 'Up', classes: defaultClasses },
    { name: 'down', text: 'Down', classes: defaultClasses },
    { name: 'undo', text: 'Undo', classes: 'col-12 col-md-6 col-lg-2 mb-3 mb-lg-0' },
    { name: 'solve', text: 'Solve', classes: 'col-12 col-md-6 col-lg-2 mb-lg-0'}
  ]

  const buttons = buttonConfigs.map(config => {
    const onClick = () => handleAction(config.name)

    return (
      <div key={config.name} className={config.classes}>
        <button className='form-control' {...{ onClick }}>{config.text}</button>
      </div>
    )
  })

  return (
    <div className='row mb-4'>
      { buttons }
    </div>
  )

}

export default ActionBar

import React from 'react'

export const ActionBar = ({ handleAction }) => {

  const defaultClasses = 'col-12 col-md-3 col-lg-2'

  const buttonConfigs = [
    { text: 'Left' },
    { text: 'Right' },
    { text: 'Up' },
    { text: 'Down' },
    { text: 'Undo', classes: 'col-12 col-md-6 col-lg-2' },
    { text: 'Solve', classes: 'col-12 col-md-6 col-lg-2'}
  ]

  const buttons = buttonConfigs.map(config => {
    const name = config.name ? config.name : config.text.toLowerCase()
    const onClick = () => handleAction(name)

    return (
      <div key={name} className={config.classes ? config.classes : defaultClasses}>
        <button className='form-control' {...{ onClick }}>{config.text}</button>
      </div>
    )
  })

  return (
    <div className='row'>
      { buttons }
    </div>
  )

}

export default ActionBar

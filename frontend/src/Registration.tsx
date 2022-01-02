import React, { useState } from 'react';
import axios from 'axios';

type State = {
    name: string
}

export const RegistrationComponent = () => {
  const [name, setName] = useState<string>("");

  const onRegisterButtonClicked = () => {
    axios({
      method: 'get',
      url: 'http://localhost:5000/api/user/getall',
      data: {
        name
      }
    })
    .then(() => console.log("success"))
    .catch(err => console.error(err));
  }

  return (
    <div className="App">
      <label htmlFor="name">Введите ваше имя</label>
      <input id="name" type="text" onChange={(ev: React.ChangeEvent<HTMLInputElement>) => setName(ev.target.value)} />

      <button onClick={() => onRegisterButtonClicked()}>Зарегистрироваться</button>
    </div>
  );
}

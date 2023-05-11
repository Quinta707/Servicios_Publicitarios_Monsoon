import { useRouter } from 'next/router';
import React, { useContext, useState } from 'react';
import AppConfig from '../../../layout/AppConfig';
import { Checkbox } from 'primereact/checkbox';
import { Button } from 'primereact/button';
import { Password } from 'primereact/password';
import { LayoutContext } from '../../../layout/context/layoutcontext';
import { InputText } from 'primereact/inputtext';
import { classNames } from 'primereact/utils';
import axios from 'axios';
import Global from '../../api/Global';

const RecovPage = () => {
    const [password, setPassword] = useState('');
    const [checked, setChecked] = useState(false);
    const { layoutConfig } = useContext(LayoutContext);

    const router = useRouter();
    const containerClassName = classNames('surface-ground flex align-items-center justify-content-center min-h-screen min-w-screen overflow-hidden', { 'p-input-filled': layoutConfig.inputStyle === 'filled' });

    const [usuario, setusuario] = useState('');
    const [pass, setpass] = useState('');
    const [newpass, setnewpass] = useState('');

    const [submitted, setsubmitted] = useState(false);
    const [sub2, setsub2] = useState(false);
    const [incorrectpass, setincorrectpass] = useState(false);
    const [validationErrors, setValidationErrors] = useState({});

    const repasAction = (e) => {
        if(usuario == "" || usuario == null || pass == "" || pass == null || newpass == "" || newpass == null )
        {
            setsubmitted(true)
        }
        else{
        console.log(localStorage.getItem('token'))
        let payload = {
            user_NombreUsuario: usuario,
        }
        
        axios.put(Global.url + 'Usuario/ValidarUsuario', payload)
        .then((r) => {
            const data = r.data;
            if(data != "" && data != null){
                if(pass == newpass){
                    let insert = {
                        user_NombreUsuario: usuario,
                        user_Contrasena:    pass,
                    }
                    axios.put(Global.url + 'Usuario/Recuperar', insert)
                    .then((r) => {
                        localStorage.setItem('token', data.user_Id);
                        console.log(localStorage.getItem('token'));
                        router.push('../auth/login');
                    })
                }
                else{
                    setincorrectpass(true)
                }
                setsub2(false)
            }
            else(
                setsub2(true)
            )
        })
        .catch((e) => {
            if (e.response.data.errors != undefined) {
                setValidationErrors(e.response.data.errors);
            }
            if (e.response.data.error != undefined) {
                setValidationErrors(e.response.data.error);
            }
        });
        }
        
    }

    return (
        <div className={containerClassName}>
            <div className="surface-ground background-image flex flex-column align-items-center justify-content-center">
                <div style={{ borderRadius: '56px', padding: '0.3rem', background: 'linear-gradient(180deg, var(--primary-color) 10%, rgba(33, 150, 243, 0) 30%)' }}>
                    <div className="w-full surface-card py-8 px-5 sm:px-8" style={{ borderRadius: '53px' }}>
                        <div className="text-center mb-5">
                            <img src="/demo/images/login/cris.png" alt="Image" height="300" className="mb-3" />
                            <div className="text-900 text-3xl font-medium mb-3">¿Olvidaste tu contraseña?</div>
                            <span className="text-600 font-medium"> Recuperar contraseña</span>
                        </div>
                        {sub2 && <small className="p-invalid" style={{color: 'red'}}>El usuario ingresado no existe.</small>}
                        {incorrectpass && <small className="p-invalid" style={{color: 'red'}}>Las contraseñas no coinciden.</small>}
                        <div>
                            <label htmlFor="email1" className="block text-900 text-xl font-medium mb-2">
                                Usuario
                            </label>
                            {submitted && !usuario && <small className="p-invalid" style={{color: 'red'}}>El campo es requerido.</small>}
                            <InputText value={usuario} onChange={(e)=>{setusuario(e.target.value)}} placeholder='Ingrese su usuario' className={ 'w-full mb-5 p-inputtext p-component' + classNames({ 'p-invalid': submitted && !usuario })}></InputText>

                            <label htmlFor="password1" className="block text-900 font-medium text-xl mb-2">
                                Nueva contraseña
                            </label>
                            {submitted && !pass && <small className="p-invalid" style={{color: 'red'}}>El campo es requerido.</small>}
                            <input type='password' value={pass} onChange={(e) => setpass(e.target.value)}
                            placeholder="Ingrese su nueva contraseña" className={ 'w-full mb-5 p-inputtext p-password-input p-component' + classNames({ 'p-invalid': submitted && !pass })} icon="pi pi-eye"/>

                            <label htmlFor="password1" className="block text-900 font-medium text-xl mb-2">
                                Confirmar nueva contraseña
                            </label>
                            {submitted && !newpass && <small className="p-invalid" style={{color: 'red'}}>El campo es requerido.</small>}
                            <input type='password' value={newpass} onChange={(e) => setnewpass(e.target.value)}
                            placeholder="Confirme su nueva contraseña" className={ 'w-full mb-5 p-inputtext p-password-input p-component' + classNames({ 'p-invalid': submitted && !newpass })} icon="pi pi-eye"/>
                            
                            <div className="flex align-items-center justify-content-between mb-5 gap-5">
                                
                                <a className="font-medium no-underline ml-2 text-right cursor-pointer" style={{ color: 'var(--primary-color)' }} onClick={() => router.push('../auth/login')}>
                                    Regresar al login
                                </a>
                            </div>
                            <Button label="Guardar" className="w-full p-3 text-xl" onClick={((e) => repasAction(e))}></Button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};

RecovPage.getLayout = function getLayout(page) {
    return (
        <React.Fragment>
            {page}
            <AppConfig simple />
        </React.Fragment>
    );
};
export default RecovPage;

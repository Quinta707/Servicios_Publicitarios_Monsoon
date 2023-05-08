import { useRouter } from 'next/router';
import React, { useContext, useState, useEffect } from 'react';
import AppConfig from '../../../layout/AppConfig';
import { Checkbox } from 'primereact/checkbox';
import { Button } from 'primereact/button';
import { Password } from 'primereact/password';
import { LayoutContext } from '../../../layout/context/layoutcontext';
import { InputText } from 'primereact/inputtext';
import { classNames } from 'primereact/utils';
import axios from 'axios';
import { useRoute } from 'next/router'
import Dashboard from '../../index'
import crud  from '../../pages/crud/index';

function LoginPage(){
    //const navigate = useNavigate();
    const [usuario, setusuario] = useState("")
    const [clave, setclave] = useState("")
    const [validationErrors, setValidationErrors] = useState({});
    const [isSubmitting, setIsSubmitting] = useState(false);

    const [checked, setChecked] = useState(false);
    const { layoutConfig } = useContext(LayoutContext);

    const router = useRouter();
    const containerClassName = classNames('surface-ground flex align-items-center justify-content-center min-h-screen min-w-screen overflow-hidden', { 'p-input-filled': layoutConfig.inputStyle === 'filled' });

    
    useEffect(()=>{
        
        if(localStorage.getItem('token') != "" && localStorage.getItem('token') != null){
           //navigate('/Dashboard', {replace: true});
           router.push('/uikit/charts');
        }
        console.log(localStorage.getItem('token'))
    },[])

    const loginAction = (e) => {
        console.log(localStorage.getItem('token'))
        setValidationErrors({})
        e.preventDefault();
        setIsSubmitting(true)
        console.log(usuario, clave)
        let payload = {
            user_NombreUsuario:usuario,
            user_Contrasena:clave,
        }
        
        axios.put('https://localhost:44304/api/Usuario/IniciarSesion', payload)
        .then((r) => {
            
            const data = r.data;
            localStorage.setItem('token', data.user_NombreUsuario);
            console.log(localStorage.getItem('token'));
            setIsSubmitting(false);
            router.push('/uikit/charts');
        })
        .catch((e) => {
            setIsSubmitting(false)
            if (e.response.data.errors != undefined) {
                setValidationErrors(e.response.data.errors);
            }
            if (e.response.data.error != undefined) {
                setValidationErrors(e.response.data.error);
            }
        });
    }


    return (
        <div className={containerClassName}>
            <div className="surface-ground background-image flex flex-column align-items-center justify-content-center">
                <div style={{ borderRadius: '56px', padding: '0.3rem', background: 'linear-gradient(180deg, var(--primary-color) 10%, rgba(33, 150, 243, 0) 30%)' }}>
                    <div className="w-full surface-card py-8 px-5 sm:px-8" style={{ borderRadius: '53px' }}>
                        <div className="text-center mb-5">
                            <img src="/demo/images/login/prueba.png" alt="Image" height="250" className="mb-3" />
                            <div className="text-900 text-3xl font-medium mb-3">Servicios Publicitarios Monsoon</div>
                            <span className="text-600 font-medium"> Iniciar Sesión</span>
                        </div>
                        <form onSubmit={(e)=>{loginAction(e)}}>
                                {Object.keys(validationErrors).length != 0 &&
                                    <p className='text-center '><small className='text-danger'>Incorrect usuario or Password</small></p>
                                }
                        <div>
                        <label htmlFor="usuario" className="block text-900 font-medium text-xl mb-2">
                            Usuario
                            </label>
                            <label id="errorusu"></label>
                            <input type="text" id="usuario" name="usuario" value={usuario} onChange={(e)=>{setusuario(e.target.value)}} placeholder='Ingrese su usuario' toggleMask className="w-full mb-5 p-inputtext p-component" />
                            <label htmlFor="clave" className="block text-900 font-medium text-xl mb-2">
                                Contraseña
                            </label>
                            <label id="errorpass"></label>
                            <input inputid="clave" id="clave" type='password' name="clave" value={clave} onChange={(e) => setclave(e.target.value)} placeholder="Ingrese su contraseña" toggleMask className="w-full mb-5 p-inputtext p-component" ></input>

                            <div className="flex align-items-center justify-content-between mb-5 gap-5">
                                <div className="flex align-items-center">
                                    <Checkbox inputid="rememberme1" checked={checked} onChange={(e) => setChecked(e.checked)} className="mr-2"></Checkbox>
                                    <label htmlFor="rememberme1">Recordarme</label>
                                </div>
                                <a className="font-medium no-underline ml-2 text-right cursor-pointer" style={{ color: 'var(--primary-color)' }} >
                                    ¿Olvidaste tu contraseña?
                                </a>
                            </div>
                            <button type="submit" className='p-button w-full mb-5 text-center'>Ingresar</button>
                            
                        </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    );
};

LoginPage.getLayout = function getLayout(page) {
    return (
        <React.Fragment>
            {page}
            <AppConfig simple />
        </React.Fragment>
    );
};


export default LoginPage;
import { useRouter } from 'next/router'
import React, { useState, useEffect, useRef } from 'react';
import axios from 'axios';
import Global from '../../api/Global';
import { Fieldset } from 'primereact/fieldset';
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import { Button } from 'primereact/button';


const Detalle = () => {

    const router = useRouter();
    const [searchText, setSearchText] = useState(''); //para la barra de busqueda
    const [FacturaId, setFacturaId] = useState(router.query.id); // optiene el id del resgitro a editar

    const [user_Creacion, setuser_Creacion] = useState('');
    const [FechaCreacion, setFechaCreacion] = useState('');
    const [user_Modificacion, setuser_Modificacion] = useState('');
    const [FechaModificacion, setFechaModificacion] = useState('');

    const [Auditoria, setAuditoria] = useState([]);

    const [FacturaDatos, setFacturaDatos] = useState([]);
    const [cliente, setcliente] = useState('');
    const [Empleado, setEmpleado] = useState('');
    const [sucursal, setSucursal] = useState('');
    const [metodopago, setmetodopago] = useState('');
    const [FechaCompra, setFechaCompra] = useState('');
    const [SubTotal, setSubTotal] = useState('');
    const [IVA, setIVA] = useState('');
    const [Total, setTotal] = useState('');


    useEffect(() => {
                setcliente('Mauricio Mateo')
                setEmpleado('Cristian Aguilar')
                setSucursal('Sucursal de sps')
                setmetodopago('Tarjeta')
                setFechaCompra('2023-11-05')

    }, []);

    return (

        <div>
            <div>

                <div className="card" style={{ background: `rgb(105,101,235)`, height: '100px', width: '100%' }}>
                    <div className="row text-center d-flex align-items-center">
                        <h2 style={{ color: 'white' }}>Detalles</h2>
                    </div>
                </div>

                <div className="card">
                    <div className='col-12'>
                        <div className="row text-center d-flex align-items-center">
                            <div className='p-fluid formgrid grid'>
                                <div className='field col-12 md:col-4'>
                                <Button label="Factura Id" rounded /><label>1</label>
                                </div>
                                <div className='field col-12 md:col-4'>
                                <Button label="Cliente" rounded />
                                <label>{cliente}</label>
                                </div>
                                <div className='field col-12 md:col-4'>
                                <Button label="Empleado" rounded /> <label>{Empleado}</label>
                                </div>
                            </div>
                            <div className='p-fluid formgrid grid'>
                                <div className='field col-12 md:col-4'>
                                <Button label="Sucursal" rounded /> <label>{sucursal}</label>
                                </div>
                                <div className='field col-12 md:col-4'>
                                <Button label="Primary" rounded /> <label>{metodopago}</label>
                                </div>
                                <div className='field col-12 md:col-4'>
                                <Button label="Fecha de Compra" rounded /> <label>{FechaCompra}</label>
                                </div>
                            </div>

                        </div>
                    </div>
                    
                </div>
            </div>
        </div>
    )

}

export default Detalle;






// import { useRouter } from 'next/router';
// import React, { useContext, useState, useEffect } from 'react';
// import AppConfig from '../../../layout/AppConfig';
// import { Checkbox } from 'primereact/checkbox';
// import { LayoutContext } from '../../../layout/context/layoutcontext';
// import { classNames } from 'primereact/utils';
// import axios from 'axios';
// import { useRoute } from 'next/router'
// import Global from '../../api/Global';
// import { InputText } from 'primereact/inputtext';
// import { Password } from 'primereact/password';


// function LoginPage(){
//     //const navigate = useNavigate();
//     const [usuario, setusuario] = useState("")
//     const [clave, setclave] = useState("")
//     const [validationErrors, setValidationErrors] = useState({});

//     const [checked, setChecked] = useState(false);
//     const { layoutConfig } = useContext(LayoutContext);

//     const router = useRouter();
//     const containerClassName = classNames('surface-ground flex align-items-center justify-content-center min-h-screen min-w-screen overflow-hidden', { 'p-input-filled': layoutConfig.inputStyle === 'filled' });
//     const [submitted, setsubmitted] = useState(false);
//     const [sub2, setsub2] = useState(false);
    
//     useEffect(()=>{
        
//         if(localStorage.getItem('usuID') != "" && localStorage.getItem('usuID') != null){
//            router.push('/uikit/charts');
//         }
//         console.log(localStorage.getItem('usuID'))
//     },[])

//     const loginAction = (e) => {
//         if(usuario == "" || usuario == null || clave == "" || clave == null )
//         {
//             setsubmitted(true)
//         }
//         else{
//         console.log(localStorage.getItem('usuID'))
//         setValidationErrors({})
//         e.preventDefault();
//         let payload = {
//             user_NombreUsuario:usuario,
//             user_Contrasena:clave,
//         }
        
//         axios.put(Global.url + 'Usuario/IniciarSesion', payload)
//         .then((r) => {
//             const data = r.data;
//             if(data != "" && data != null){
//                 localStorage.setItem('usuID', data.user_Id);
//                 console.log(localStorage.getItem('usuID'));
//                 router.push('/uikit/charts');
//             }
//             else(
//                 setsub2(true)
//             )
//         })
//         .catch((e) => {
//             if (e.response.data.errors != undefined) {
//                 setValidationErrors(e.response.data.errors);
//             }
//             if (e.response.data.error != undefined) {
//                 setValidationErrors(e.response.data.error);
//             }
//         });
//         }
        
//     }


//     return (
//         <div className={containerClassName}>
//             <div className="surface-ground background-image flex flex-column align-items-center justify-content-center">
//                 <div style={{ borderRadius: '56px', padding: '0.3rem', background: 'linear-gradient(180deg, var(--primary-color) 10%, rgba(33, 150, 243, 0) 30%)' }}>
//                     <div className="w-full surface-card py-8 px-5 sm:px-8" style={{ borderRadius: '53px' }}>
//                         <div className="text-center mb-5">
//                             <img src="/demo/images/login/prueba.png" alt="Image" height="250" className="mb-3" />
//                             <div className="text-900 text-3xl font-medium mb-3">Servicios Publicitarios Monsoon</div>
//                             <span className="text-600 font-medium"> Iniciar Sesión</span>
//                         </div>
//                         <div className='text-center'>
//                         {sub2 && <small className="p-invalid" style={{color: 'red'}}>Usuario o contraseña incorrectos.</small>}
//                         </div>
//                         <div>
//                         <label htmlFor="usuario" className="block text-900 font-medium text-xl mb-2">
//                             Usuario
//                         </label>
//                             {submitted && !usuario && <small className="p-invalid" style={{color: 'red'}}>El campo es requerido.</small>}
//                             <InputText value={usuario} onChange={(e)=>{setusuario(e.target.value)}} placeholder='Ingrese su usuario' className={ 'w-full mb-5 p-inputtext p-component' + classNames({ 'p-invalid': submitted && !usuario })}></InputText>
//                         <label htmlFor="clave" className="block text-900 font-medium text-xl mb-2">
//                              Contraseña
//                         </label>
//                         {submitted && !clave && <small className="p-invalid" style={{color: 'red'}}>El campo es requerido.</small>}
//                             <input type='password' value={clave} onChange={(e) => setclave(e.target.value)}
//                             placeholder="Ingrese su contraseña" className={ 'w-full mb-5 p-inputtext p-password-input p-component' + classNames({ 'p-invalid': submitted && !clave })} icon="pi pi-eye"/>

//                             <div className="flex align-items-center justify-content-between mb-5 gap-5">
//                                 <div className="flex align-items-center">
//                                     <Checkbox inputid="rememberme1" checked={checked} onChange={(e) => setChecked(e.checked)} className="mr-2"></Checkbox>
//                                     <label htmlFor="rememberme1">Recordarme</label>
//                                 </div>
//                                 <a className="font-medium no-underline ml-2 text-right cursor-pointer" style={{ color: 'var(--primary-color)' }} onClick={() => router.push('../auth/recoverpass')} >
//                                     ¿Olvidaste tu contraseña?
//                                 </a>
//                             </div>
//                             <button type="submit" className='p-button w-full mb-5 text-center' onClick={ ((e) => loginAction(e))}>Ingresar</button>
                            
//                         </div>
//                     </div>
//                 </div>
//             </div>
//         </div>
//     );
// };

// LoginPage.getLayout = function getLayout(page) {
//     return (
//         <React.Fragment>
//             {page}
//             <AppConfig simple />
//         </React.Fragment>
//     );
// };


// export default LoginPage;
import React, { useState, useEffect, useRef } from 'react';
import { InputText } from 'primereact/inputtext';
import { Calendar } from 'primereact/calendar';
import { RadioButton } from 'primereact/radiobutton';
import { Dropdown } from 'primereact/dropdown';
import { InputTextarea } from 'primereact/inputtextarea';
import { Button } from 'primereact/button';
import Global from '../api/Global';
import axios from 'axios'
import { useRouter } from 'next/router'
import { InputMask } from 'primereact/inputmask';
import { classNames } from 'primereact/utils';

const createEmpleado = () => {

    const router = useRouter();

    const [submitted, setSubmitted] = useState(false);  //validar campos vacios
    const [MunicipioSubmited, setMunicipioSubmited] = useState(false); //validar si municipio esta vacio

    const [Nombres, setNombres] = useState('');
    const [Apellidos, setApellidos] = useState('');
    const [Identidad, setIdentidad] = useState('');
    const [FechaNac, setFechaNac] = useState('');
    const [Sexo, setSexo] = useState('');
    const [Dirrecion, setDirrecion] = useState('');
    const [Telefono, setTelefono] = useState('');

    const [EstadosCivilesDDL, setEstadosCivilesDDL] = useState([]); //ddl estados civiles
    const [EstadoCivil, setEstadoCivil] = useState(''); //almacena el valor seleccionado del ddl 

    const [SucursalDDL, setSucursalDDL] = useState([]); //ddl Sucursal
    const [Sucursal, setSucursal] = useState(''); //almacena el valor seleccionado del ddl 

    const [CargoDDL, setCargoDDL] = useState([]);//ddl Cargos
    const [Cargo, setCargo] = useState('');//almacena el valor seleccionado del ddl 

    const [DepartaemntoDDL, setDepartamentoDDL] = useState([]);//ddl Departemento 
    const [Deparatemento, setDepartamento] = useState('');//almacena el valor seleccionado del ddl 

    const [MunicipioDDL, setMunicipioDDL] = useState([]);//ddl Municipios
    const [Municipio, setMunicipio] = useState(''); // alamcena el valor del ddl
    const [MunicipioActivated, setMunicipioActivated] = useState(true);// almacena si el ddl esta activado

    //cargar ddl Cargos
    useEffect(() => {
        axios.get(Global.url + 'EstadoCivil/Listado')
            .then(response => response.data)
            .then((data) => setEstadosCivilesDDL(data.data.map((c) => ({ code: c.eciv_Id, name: c.eciv_Descripcion }))))
            .catch(error => console.error(error))

        axios.get(Global.url + 'Sucursal/Listado')
            .then(response => response.data)
            .then((data) => setSucursalDDL(data.data.map((c) => ({ code: c.sucu_Id, name: c.sucu_Nombre }))))
            .catch(error => console.error(error))

        axios.get(Global.url + 'Cargo/Listado')
            .then(response => response.data)
            .then((data) => setCargoDDL(data.data.map((c) => ({ code: c.carg_Id, name: c.carg_Descripcion }))))
            .catch(error => console.error(error))

        axios.get(Global.url + 'Departamento/Listado')
            .then(response => response.data)
            .then((data) => setDepartamentoDDL(data.data.map((c) => ({ code: c.depa_Id, name: c.depa_Nombre }))))
            .catch(error => console.error(error))

    }, []);


    const ActivarMunicipioDDl = (depa_Id) => {
        setMunicipioActivated(false);
        axios.put(Global.url + 'Municipio/MunicipioDDL?id=' + depa_Id)
            .then(response => response.data)
            .then((data) => setMunicipioDDL(data.data.map((c) => ({ code: c.muni_Id, name: c.muni_Nombre }))))
            .catch(error => console.error(error))
    }


    const Insetar = (e) => {

        if (!Nombres || !Apellidos || !Identidad || !FechaNac || !Sexo ||
            !EstadoCivil || !Deparatemento || !Municipio || !Dirrecion || !Telefono || !Sucursal || !Cargo) {
            setSubmitted(true);

            if (Deparatemento && !Municipio) {
                setMunicipioSubmited(true);
            }
        }
        else {


            let empleado = {
                empe_Nombres: Nombres,
                empe_Apellidos: Apellidos,
                empe_Identidad: Identidad,
                empe_FechaNacimiento: FechaNac,
                empe_Sexo: Sexo,
                eciv_Id: EstadoCivil.code,
                muni_Id: Municipio.code,
                empe_DireccionExacta: Dirrecion,
                empe_Telefono: Telefono,
                sucu_Id: Sucursal.code,
                carg_Id: Cargo.code,
                empe_UsuCreacion: 1
            }


            axios.post(Global.url + 'Empleado/Insertar', empleado)
                .then((r) => {

                    localStorage.setItem('EmpleadoInsert', '1');

                    router.push('./empleado_index')
                })
                .catch((e) => {
                    toast.current.show({ severity: 'warn', summary: 'Advertencia', detail: 'Ups, algo salió mal. ¡Inténtalo nuevamente!', life: 2000 });
                })
        }
    }

    return (
        <div className='col-12'>
            <div className="card" style={{ background: `rgb(105,101,235)`, height: '100px', width: '100%' }}>
                <div className="row text-center d-flex align-items-center">
                    <h2 style={{ color: 'white' }}>Ingresar Empleados</h2>
                </div>
            </div>

            <div className="card">
                <div className="grid p-fluid">

                    <div className="col-6">
                        <div className="field mt-3">
                            <label htmlFor="inputtext">Nombres</label><br />
                            <InputText type="text" id="inputtext" value={Nombres} onChange={(e) => setNombres(e.target.value)} className={classNames({ 'p-invalid': submitted && !Nombres })} />
                            {submitted && !Nombres && <small className="p-invalid" style={{ color: 'red' }}>El campo es requerido.</small>}
                        </div>
                    </div>

                    <div className="col-6">
                        <div className="field mt-3">
                            <label htmlFor="inputtext">Apellidos</label><br />
                            <InputText type="text" id="inputtext" value={Apellidos} onChange={(e) => setApellidos(e.target.value)} className={classNames({ 'p-invalid': submitted && !Apellidos })} />
                            {submitted && !Apellidos && <small className="p-invalid" style={{ color: 'red' }}>El campo es requerido.</small>}
                        </div>
                    </div>

                    <div className="col-6">
                        <div className="field">
                            <label htmlFor="inputtext">Identidad</label><br />
                            <InputMask id="inputmaskIdentidad" mask="9999-9999-99999" slotChar="0000-0000-00000" value={Identidad} onChange={(e) => setIdentidad(e.target.value)} className={classNames({ 'p-invalid': submitted && !Identidad })} />
                            {submitted && !Identidad && <small className="p-invalid" style={{ color: 'red' }}>El campo es requerido.</small>}
                        </div>
                    </div>

                    <div className="col-6">
                        <div className="field">
                            <label htmlFor="calendar">Fecha Nacimiento</label>
                            <Calendar inputId="calendar" showIcon value={FechaNac} onChange={(e) => setFechaNac(e.target.value)} className={classNames({ 'p-invalid': submitted && !FechaNac })} />
                            {submitted && !FechaNac && <small className="p-invalid" style={{ color: 'red' }}>El campo es requerido.</small>}
                        </div>
                    </div>

                    <div className="col-6">
                        <div className="field">
                            <label htmlFor="Sexo">Sexo</label><br />
                            <div className="grid">
                                <div className="col-12 md:col-4">
                                    <div className="field-radiobutton">
                                        <RadioButton inputId="option1" name="option" value="F" checked={Sexo === 'F'} onChange={(e) => setSexo(e.value)} className={classNames({ 'p-invalid': submitted && !Sexo })} />
                                        <label htmlFor="option1">Femenino</label>
                                    </div>
                                </div>
                                <div className="col-12 md:col-4">
                                    <div className="field-radiobutton">
                                        <RadioButton inputId="option2" name="option" value="M" checked={Sexo === 'M'} onChange={(e) => setSexo(e.value)} className={classNames({ 'p-invalid': submitted && !Sexo })} />
                                        <label htmlFor="option2">Masulino</label>
                                    </div>
                                </div>

                            </div>
                            {submitted && !Sexo && <small className="p-invalid" style={{ color: 'red' }}>El campo es requerido.</small>}
                        </div>
                    </div>

                    <div className='col-6'>
                        <div className="field">
                            <label htmlFor="Sexo">Estado Civil</label><br />
                            <Dropdown optionLabel="name" placeholder="Selecionar" options={EstadosCivilesDDL} value={EstadoCivil} onChange={(e) => setEstadoCivil(e.value)} className={classNames({ 'p-invalid': submitted && !EstadoCivil })} />
                            {submitted && !EstadoCivil && <small className="p-invalid" style={{ color: 'red' }}>Seleccione una opcion.</small>}
                        </div>
                    </div>

                    <div className="col-6">
                        <div className="field">
                            <label htmlFor="inputtext">Telefono</label><br />
                            <InputMask id="inputmaskTelefono" mask="+990 9999-9999" slotChar="+000 0000-0000" value={Telefono} onChange={(e) => setTelefono(e.target.value)} className={classNames({ 'p-invalid': submitted && !Telefono })} />
                            {submitted && !Telefono && <small className="p-invalid" style={{ color: 'red' }}>El campo es requerido.</small>}
                        </div>
                    </div>

                    <div className='col-6'>
                        <div className="field">
                            <label htmlFor="Sucursal">Sucursal</label><br />
                            <Dropdown optionLabel="name" placeholder="Selecionar" options={SucursalDDL} value={Sucursal} onChange={(e) => setSucursal(e.value)} className={classNames({ 'p-invalid': submitted && !Sucursal })} />
                            {submitted && !Sucursal && <small className="p-invalid" style={{ color: 'red' }}>Seleccione una opcion.</small>}
                        </div>
                    </div>

                    <div className='col-6'>
                        <div className="field">
                            <label htmlFor="Sexo">Cargo</label><br />
                            <Dropdown optionLabel="name" placeholder="Selecionar" options={CargoDDL} value={Cargo} onChange={(e) => setCargo(e.value)} className={classNames({ 'p-invalid': submitted && !Cargo })} />
                            {submitted && !Cargo && <small className="p-invalid" style={{ color: 'red' }}>Seleccione una opcion.</small>}
                        </div>
                    </div>

                    <div className='col-6'>
                        <div className="field">
                            <label htmlFor="Sexo">Departamento</label><br />
                            <Dropdown optionLabel="name" placeholder="Seleccionar" options={DepartaemntoDDL} value={Deparatemento} onChange={(e) => { setDepartamento(e.value); ActivarMunicipioDDl(e.value.code); }} className={classNames({ 'p-invalid': submitted && !Deparatemento })} />
                            {submitted && !Deparatemento && <small className="p-invalid" style={{ color: 'red' }}>Seleccione una opcion.</small>}
                        </div>
                    </div>

                    <div className='col-6'>
                        <div className="field">
                            <label htmlFor="Sexo">Municipio</label><br />
                            <Dropdown optionLabel="name" placeholder="Selecionar" options={MunicipioDDL} value={Municipio} onChange={(e) => setMunicipio(e.value)} disabled={MunicipioActivated} className={classNames({ 'p-invalid': MunicipioSubmited && !Municipio })} />
                            {MunicipioSubmited && !Municipio && <small className="p-invalid" style={{ color: 'red' }}>Seleccione una opcion.</small>}
                        </div>
                    </div>

                    <div className='col-12'>
                        <div className="field">
                            <label htmlFor="Sexo">Direccion</label><br />
                            <InputTextarea placeholder="" autoResize rows="3" cols="30" value={Dirrecion} onChange={(e) => setDirrecion(e.target.value)} className={classNames({ 'p-invalid': submitted && !Dirrecion })} />
                            {submitted && !Dirrecion && <small className="p-invalid" style={{ color: 'red' }}>El campo es requerido.</small>}
                        </div>
                    </div>

                    <div className='col-6'>
                        <div className="grid p-fluid">

                            <div className='col-6'>
                                <Button label="Crear" severity="success" onClick={() => Insetar()} />
                            </div>

                            <div className='col-6'>
                                <Button label="Cancelar" severity="default" onClick={() => router.push('./empleado_index')} />
                            </div>


                        </div>
                    </div>

                </div>

            </div>
        </div>

    )
}

export default createEmpleado;
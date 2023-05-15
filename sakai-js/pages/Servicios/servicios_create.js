import React, { useState, useEffect, useRef } from 'react';
import { Button } from 'primereact/button';
import { InputText } from 'primereact/inputtext';
import { classNames } from 'primereact/utils';
import { InputNumber } from 'primereact/inputnumber';
import { DataTable } from 'primereact/datatable';
import axios from 'axios';
import { Column } from 'primereact/column';
import Global from '../api/Global';
import { Toast } from 'primereact/toast';
import { useRouter } from 'next/router'
import { Dropdown } from 'primereact/dropdown';

const Servicios = () => {
    const [submitted, setsubmitted] = useState(false);
    const [Insumo, setInsumo] = useState([]);
    const toast = useRef(null);

    const [ServicioName, setServicioName] = useState('');
    const [Precio, setPrecio] = useState('');
    const router = useRouter();
    
    const [image, setimage] = useState('');
    const [Imgurl, setImgurl] = useState('');
    const apikey = 'a033b10908e651455bbeb051e14a6d72';

    const [servicio, setservicio] = useState(false);
    const [insum, setinsum] = useState(true);

    const [CategoriaDDL, setCategoriaDDL] = useState([]);
    const [Categoria, setCategoria] = useState('');

    const [InsumoDDL, setInsumoDDL] = useState([]);
    const [Insumoval, setInsumoval] = useState('');
    const [InsumoActivated, setInsumoActivated] = useState(true);
    const [InsumoSubmited, setInsumoSubmited] = useState(false);

    useEffect(() => {
        axios.get(Global.url + 'Categoria/Listado')
        .then(response => response.data)
        .then((data) => setCategoriaDDL( data.data.map((c) => ({ code: c.cate_Id, name: c.cate_Descripcion }))))
        .catch(error => console.error(error))
    },);

    const ActivarInsumoDDl = (cate_Id) => {
        setInsumoActivated(false);
        axios.put(Global.url + 'Insumo/InsumoDDL?id='+ cate_Id)
            .then(response => response.data)
            .then((data) => setInsumoDDL( data.data.map((c) => ({ code: c.insu_Id, name: c.insu_Nombre }))))
            .catch(error => console.error(error))
    }

    const handleImageChange = (e) => {
        const file = e.target.files[0];
        if (file) {
          const reader = new FileReader();
          reader.readAsDataURL(file);
          reader.onload = () => {
            setimage(reader.result);
          };
        }
    };

    const handleImageUpload = async () => {
    const base64Image = image.split(',')[1]; // obtener la cadena Base64 sin el prefijo "data:image/png;base64,"
    const url = 'https://api.imgbb.com/1/upload?key=' + apikey;
    const body = new FormData();
    body.append('image', base64Image);
    
        const response = await  fetch(url, {
            method: 'POST',
            body: body
          })
    
        const data = await response.json();
        console.log(data)
        setImgurl(data.data.url);
        NuevoServicio();
    };

    const NuevoServicio = () => {
        if(ServicioName !== "" && ServicioName != null && Precio !== "" && Precio !== null ){

             let servicio = {
                 serv_Id:                   0,
                 serv_Nombre:               ServicioName,
                 serv_Precio:               parseInt(Precio),
                 serv_url:                  Imgurl,
                 serv_UsuCreacion:          1,
                 serv_FechaCreacion:        "2023-05-11T19:57:32.534Z",
                 serv_UsuModificacion:      0,
                 serv_FechaModificacion:    "2023-05-11T19:57:32.534Z",
                 serv_Estado:               true
             }

             axios.post(Global.url + 'Servicio/Insertar', servicio)
             .then((r) => {
                setservicio(true);
                setinsum(false);
             });
        }
        else{
            setsubmitted(true);
        }
    };

    return (
        <div className="grid">
            <div className="col-12">
                <div className="card">
                    <br></br>
                    <Toast ref={toast} />
                    <div className="surface-0 text-700 text-center">
                        <div className="text-900 text-primary font-bold text-5xl mb-3">Crea un servicio</div>
                        <br></br>
                            <div className="p-fluid formgrid grid">
                                <div className="field col-12 md:col-4">
                                    <label htmlFor="name">Nombre del servicio</label>
                                    <InputText id="name" value={ServicioName} onChange={(e) => { setServicioName(e.target.value);}} className={classNames({ 'p-invalid': submitted && !ServicioName })} disabled={servicio}/>
                                    {submitted && !ServicioName && <small className="p-invalid">Este campo es requerido.</small>}
                                </div>

                                <div className="field col-12 md:col-4">
                                    <label htmlFor="price" >Precio</label>
                                    <InputNumber id="price" value={Precio} onValueChange={(e) => { setPrecio(e.value);}} className={classNames({ 'p-invalid': submitted && !Precio })} mode="currency" currency="USD" locale="en-US" disabled={servicio} />
                                    {submitted && !Precio && <small className="p-invalid">Este campo es requerido.</small>}
                                </div>

                                <div className="field col-12 md:col-4">
                                    <label htmlFor="fail">Imagen</label>
                                    <input className='p-button' severity="primary" type="file" accept="image/*" onChange={handleImageChange} disabled={servicio} />
                                    {image && <img src={image} alt="uploaded image" />}
                                </div>  
                                
                            </div>
                            
                            <Button label='Crear Servicio' icon="pi pi-plus" type='submit' onClick={handleImageUpload} disabled={servicio}></Button>
                          
                            
                     </div>
                </div>
                <div className='col-12'>
                    <div className='card'> 
                        <div className="surface-0 text-700 text-center">
                        <div className="text-900 text-primary font-bold text-5xl mb-3">Agrega insumos</div>
                            <div className="p-fluid formgrid grid">
                                <div className='field col-12 md:col-4'>
                                    <Dropdown optionLabel="name" placeholder="Seleccionar una categoria" options={CategoriaDDL} value={Categoria} onChange={(e) => { setCategoria(e.value); ActivarInsumoDDl(e.value.code);}} className={classNames({ 'p-invalid': submitted && !Categoria })} disabled={insum}/>
                                    {submitted && !Categoria && <small className="p-invalid" style={{color: 'red'}}>Seleccione una opcion.</small>}
                                </div>
                                <div className='field col-12 md:col-4'>
                                    <div className="field">
                                        <Dropdown optionLabel="name" placeholder="Selecionar un insumo" options={InsumoDDL} value={Insumoval} onChange={(e) => setInsumoval(e.value)} disabled={InsumoActivated} className={classNames({ 'p-invalid': InsumoSubmited && !Insumoval })} />
                                        {InsumoSubmited && !Insumoval && <small className="p-invalid" style={{color: 'red'}}>Seleccione una opcion.</small>}
                                    </div>
                                </div>
                                <div className='field col-12 md:col-4'>
                                <Button label="Agregar" id='btnNuevoInsumo' icon="pi pi-plus" severity="primary" className="mr-2" disabled={insum}/>
                                </div>
                            </div>

                            <DataTable value={Insumo} id="DataTableInsumos" emptyMessage="Aún no se han agregado insumos.">
                                <Column field="insu_Id" header="ID" headerStyle={{ background: `rgb(105,101,235)`, color: '#fff' }}></Column>
                                <Column field="insu_Nombre" header="Insumo" headerStyle={{ background: `rgb(105,101,235)`, color: '#fff' }}></Column>
                                <Column field="insu_Precio" header="Precio" headerStyle={{ background: `rgb(105,101,235)`, color: '#fff' }}></Column>
                            </DataTable>
                        </div>
                    </div>
                </div>
                    
            </div>
        </div>
        
    );
};

export default Servicios;

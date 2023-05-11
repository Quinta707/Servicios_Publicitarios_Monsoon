import React, { useState, useEffect, useRef } from 'react';
import { DataView, DataViewLayoutOptions } from 'primereact/dataview';
import { Button } from 'primereact/button';
import { MonsoonService } from '../../demo/service/MonsoonService';
import { InputText } from 'primereact/inputtext';
import { Dialog } from 'primereact/dialog';
import { classNames } from 'primereact/utils';
import { InputTextarea } from 'primereact/inputtextarea';
import { InputNumber } from 'primereact/inputnumber';
import { FileUpload } from 'primereact/fileupload';
import { DataTable } from 'primereact/datatable';
import axios from 'axios';
import { Column } from 'primereact/column';
import Global from '../api/Global';
import { Toast } from 'primereact/toast';
import { useRouter } from 'next/router'

const Servicios = () => {
    const [submitted, setsubmitted] = useState(false);
    const [Insumo, setInsumo] = useState([]);
    const toast = useRef(null);

    const [ServicioName, setServicioName] = useState('');
    const [Precio, setPrecio] = useState('');
    const router = useRouter();
    
    const [image, setImage] = useState(null);

    useEffect(() => {
        
    },);

    const handleImageChange = (event) => {
        setImage(event.target.files[0]);
      };

    const handleImageUpload = async () => {
    const formData = new FormData();
    formData.append("image", image);

    const response = await fetch("https://api.imgbb.com/1/upload?key=30e4067e1dd236eefe264d0d784c0000", {
      method: "POST",
      body: formData,
    });

    const data = await response.json();
    console.log(data);
    };

    const NuevoServicio = (e) => {
        if(ServicioName != "" || ServicioName != null || Precio != "" || Precio != null){
            const usuario = localStorage.getItem('token');
            handleImageUpload;
            // let servicio = {
            //     serv_Id: 0,
            //     serv_Nombre: ServicioName,
            //     serv_Precio: parseInt(Precio),
            //     serv_UsuCreacion: 1,
            //     serv_FechaCreacion: "2023-05-11T19:57:32.534Z",
            //     serv_UsuModificacion: 0,
            //     serv_FechaModificacion: "2023-05-11T19:57:32.534Z",
            //     serv_Estado: true
            // }

            // axios.post(Global.url + 'Servicio/Insertar', servicio)
            // .then((r) => {
                
                
            // });
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
                            <div className="field col-12 md:col-6">
                                <label htmlFor="name">Nombre del servicio</label>
                                <InputText id="name" value={ServicioName} onChange={(e) => { setServicioName(e.value);}} className={classNames({ 'p-invalid': submitted && !ServicioName })} />
                                {submitted && !ServicioName && <small className="p-invalid">Este campo es requerido.</small>}
                            </div>

                            <div className="field col-12 md:col-6">
                                <label htmlFor="price" >Precio</label>
                                <InputNumber id="price" value={Precio} onValueChange={(e) => { setPrecio(e.value);}} className={classNames({ 'p-invalid': submitted && !Precio })} mode="currency" currency="USD" locale="en-US"  />
                                {submitted && !Precio && <small className="p-invalid">Este campo es requerido.</small>}
                            </div>

                            <div className="field col-12">
                                <label htmlFor="fail">Imagen</label>
                                <FileUpload id="fail" type="file" onChange={handleImageChange} />
                            </div>  
                            
                        </div>
                        <Button label='Crear Servicio' icon="pi pi-plus" onClick={ () => NuevoServicio()}></Button>
                        <br></br>
                        <br></br>
                        <div> 
                            <div className="p-fluid formgrid grid">
                                <div className='field col-12 md: col-2'>
                                <Button label="Agregar un insumo" id='btnNuevoInsumo' icon="pi pi-plus" severity="sucess" className="mr-2" />
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

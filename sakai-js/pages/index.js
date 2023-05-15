import { Button } from 'primereact/button';
import { Column } from 'primereact/column';
import { DataTable } from 'primereact/datatable';
import { Menu } from 'primereact/menu';
import React, { useContext, useEffect, useRef, useState } from 'react';
import { ProductService } from '../demo/service/ProductService';
import { LayoutContext } from '../layout/context/layoutcontext';
import Link from 'next/link';
import { Chart } from 'primereact/chart';
import axios from 'axios'
import 'chartjs-plugin-datalabels';



const Dashboard = () => {
  const menu2 = useRef(null);
  const [pieData, setPieData] = useState({
    labels: [],
    datasets: [
      {
        data: [],
        backgroundColor: [],
        hoverBackgroundColor: [],
      },
    ],
  });

  useEffect(() => {
    const documentStyle = getComputedStyle(document.documentElement);

    axios.get('http://serviciospublicitarios.somee.com/api/Sucursal/GraficaSucursal').then((response) => {
      const data = response.data;
      const labels = data.map((item) => item.sucu_Nombre);
      const values = data.map((item) => item.total_cantidad);
      

      setPieData((prevData) => ({
        ...prevData,
        labels: labels,
        datasets: [
          {
            data: values,
            backgroundColor: [
              documentStyle.getPropertyValue('--indigo-500'),
              documentStyle.getPropertyValue('--purple-500'),
              documentStyle.getPropertyValue('--teal-500'),
            ],
            hoverBackgroundColor: [
              documentStyle.getPropertyValue('--indigo-400'),
              documentStyle.getPropertyValue('--purple-400'),
              documentStyle.getPropertyValue('--teal-400'),
            ],
          },
        ],
      }));
    });
  }, []);

  const options = {
    plugins: {
      legend: {
        labels: {
          usePointStyle: true,
        },
      },
      datalabels: {
        color: '#fff',
        font: {
          weight: 'bold',
        },
        formatter: (value, context) => {
          const dataset = context.chart.data.datasets[context.datasetIndex];
          const sum = dataset.data.reduce((a, b) => a + b, 0);
          const percentage = ((value * 100) / sum).toFixed(2);
          return percentage + '%';
        },
        display: 'auto',
      },
    },
  };
  return (
    <div className="grid">
      <div className="col-12 lg:col-6 xl:col-3">
        <div className="card mb-0">
          <div className="flex justify-content-between mb-3">
            <div>
              <span className="block text-500 font-medium mb-3">Termos y tazas personalizados</span>
              <div className="text-900 font-medium text-xl">50 .Lps</div>
            </div>
            <div className="flex align-items-center justify-content-center bg-blue-100 border-round" style={{ width: '2.5rem', height: '2.5rem' }}>
              <i className="pi pi-shopping-cart text-blue-500 text-xl" />
            </div>
          </div>
          <span className="text-green-500 font-medium">20 Nuevos Estilos</span>
        </div>
      </div>
      <div className="col-12 lg:col-6 xl:col-3">
        <div className="card mb-0">
          <div className="flex justify-content-between mb-3">
            <div>
              <span className="block text-500 font-medium mb-3">Prendas personalizadas</span>
              <div className="text-900 font-medium text-xl">50 .Lps</div>
            </div>
            <div className="flex align-items-center justify-content-center bg-blue-100 border-round" style={{ width: '2.5rem', height: '2.5rem' }}>
              <i className="pi pi-shopping-cart text-blue-500 text-xl" />
            </div>
          </div>
          <span className="text-green-500 font-medium">200 Nuevos Estilos</span>
        </div>
      </div>
      <div className="col-12 lg:col-6 xl:col-3">
        <div className="card mb-0">
          <div className="flex justify-content-between mb-3">
            <div>
              <span className="block text-500 font-medium mb-3">Agendas personalizadas</span>
              <div className="text-900 font-medium text-xl">50 .Lps</div>
            </div>
            <div className="flex align-items-center justify-content-center bg-blue-100 border-round" style={{ width: '2.5rem', height: '2.5rem' }}>
              <i className="pi pi-shopping-cart text-blue-500 text-xl" />
            </div>
          </div>
          <span className="text-green-500 font-medium">100 Nuevos Estilos</span>
        </div>
      </div>
      <div className="col-12 lg:col-6 xl:col-3">
        <div className="card mb-0">
          <div className="flex justify-content-between mb-3">
            <div>
              <span className="block text-500 font-medium mb-3">Placas y retrateras personalizadas</span>
              <div className="text-900 font-medium text-xl">30 .Lps</div>
            </div>
            <div className="flex align-items-center justify-content-center bg-blue-100 border-round" style={{ width: '2.5rem', height: '2.5rem' }}>
              <i className="pi pi-shopping-cart text-blue-500 text-xl" />
            </div>
          </div>
          <span className="text-green-500 font-medium">85 Nuevos Estilos</span>
        </div>
      </div>
      <div className="col-12 xl:col-6">
        <div className="card flex flex-column">
          <h5 className="text-left w-full">Ventas por Sucursales</h5>
          <Chart type="pie" data={pieData} options={options} />
        </div>
      </div>

      <div className="card col-12 xl:col-6 mt-2">
        <div className="flex align-items-center justify-content-between mb-4">
          <h5>Notificaciones</h5>
          <div>
            <Button type="button" icon="pi pi-ellipsis-v" className="p-button-rounded p-button-text p-button-plain" onClick={(event) => menu2.current.toggle(event)} />
            <Menu
              ref={menu2}
              popup
              model={[
                { label: 'Add New', icon: 'pi pi-fw pi-plus' },
                { label: 'Remove', icon: 'pi pi-fw pi-minus' }
              ]}
            />
          </div>
        </div>

        <span className="block text-600 font-medium mb-3">Hoy</span>
        <ul className="p-0 mx-0 mt-0 mb-4 list-none">
          <li className="flex align-items-center py-2 border-bottom-1 surface-border">
            <div className="w-3rem h-3rem flex align-items-center justify-content-center bg-blue-100 border-circle mr-3 flex-shrink-0">
              <i className="pi pi-dollar text-xl text-blue-500" />
            </div>
            <span className="text-900 line-height-3">
              Marco Torres
              <span className="text-700">
                {' '}
                Ordeno 20 Prendas personalizadas <span className="text-blue-500"></span>
              </span>
            </span>
          </li>
          <li className="flex align-items-center py-2">
            <div className="w-3rem h-3rem flex align-items-center justify-content-center bg-orange-100 border-circle mr-3 flex-shrink-0">
              <i className="pi pi-download text-xl text-orange-500" />
            </div>
            <span className="text-700 line-height-3">
              Las Ventas subieron un 25%<span className="text-blue-500 font-medium"></span> este mes.
            </span>
          </li>
        </ul>

        <span className="block text-600 font-medium mb-3">AYER</span>
        <ul className="p-0 m-0 list-none">
          <li className="flex align-items-center py-2 border-bottom-1 surface-border">
            <div className="w-3rem h-3rem flex align-items-center justify-content-center bg-blue-100 border-circle mr-3 flex-shrink-0">
              <i className="pi pi-dollar text-xl text-blue-500" />
            </div>
            <span className="text-900 line-height-3">
              Esdra Cerna Solicito 10 Banners
              <span className="text-700">
                {' '}
                  <span className="text-blue-500"></span>
              </span>
            </span>
          </li>
          <li className="flex align-items-center py-2 border-bottom-1 surface-border">
            <div className="w-3rem h-3rem flex align-items-center justify-content-center bg-pink-100 border-circle mr-3 flex-shrink-0">
              <i className="pi pi-dollar text-xl text-pink-500" />
            </div>
            <span className="text-900 line-height-3">
              Proveedor: 
              <span className="text-700"> la orden de insumos llegara mañana.</span>
            </span>
          </li>
        </ul>
      </div>

    </div>
  );
};

export default Dashboard;




import { ISidebarData } from "./helper";
import { SublevelBarComponent } from "./sublevel-bar.component";

export const sidebarData: ISidebarData[] = [
    {
        routeLink: 'Dashboard',
        icon: 'dashboard',
        label: 'Dashboard',
        //sublevel: 0
    },    
    {
      routeLink: 'Solicitacoes',
      icon: 'search',
      label: 'Solicitações',
      //sublevel: 0,
      items: [
        {
          routeLink: 'Geracao',
          icon: 'fiber_manual_record',
          label: 'Gerar Solicitação',
          //sublevel: 1,
        },
        {
          routeLink: 'Validacao',
          icon: 'fiber_manual_record',
          label: 'Validar Solicitação',
          //sublevel: 1
        },
        {
          routeLink: 'Acessos',
          icon: 'fiber_manual_record',
          label: 'Criar Acessos',
          //sublevel: 1
        },
        {
          routeLink: 'Visualizacao',
          icon: 'fiber_manual_record',
          label: 'Visualizar Acessos',
          //sublevel: 1
        },
        {
          routeLink: 'Acompanhamento',
          icon: 'fiber_manual_record',
          label: 'Acompanhar Solicitações',
          //sublevel: 1
        }
      ]
    },
    {
        routeLink: 'Cadastro',
        icon: 'settings',
        label: 'Cadastro',
        items: [
            {
              routeLink: 'empresas',
              icon: 'fiber_manual_record',
              label: 'Cadastro de Empresas',
              //sublevel: 1,
            },
            {
              routeLink: 'Cargos',
              icon: 'fiber_manual_record',
              label: 'Cadastro de Cargos',
              //sublevel: 1
            },
            {
              routeLink: 'Departamentos',
              icon: 'fiber_manual_record',
              label: 'Cadastro de Departamentos',
              //sublevel: 1
            },
            {
              routeLink: 'Usuarios',
              icon: 'fiber_manual_record',
              label: 'Gestão de Usuários',
              //sublevel: 1
            }
        ]
    },
    {
        routeLink: 'Relatorios',
        icon: 'computer',
        label: 'Relatórios',        
    },
    {
        routeLink: 'RedefSenha',
        icon: 'lock',
        label: 'Trocar Senha',        
    },
    {
        routeLink: 'Logout',
        icon: 'keyboard_return',
        label: 'Logout',        
    }
];

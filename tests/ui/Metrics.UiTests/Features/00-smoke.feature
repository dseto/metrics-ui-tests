# language: pt
@e2e @smoke
Funcionalidade: Smoke - subir app e autenticar

  Cenário: Login básico
    Dado que eu estou autenticado como "ADMIN_USERNAME"
    Então eu devo ver a home carregada

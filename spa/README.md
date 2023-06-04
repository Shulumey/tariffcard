Это фронтенд-приложение для тарифной карты, сгенерированное с [Angular CLI](https://github.com/angular/angular-cli) версии 10.0.6.

Запуск девелопмент-сервера
`ng serve`

Генерация нового компонента
`ng generate component component-name` или `ng generate directive|pipe|service|class|guard|interface|enum|module`

Сборка
`ng build`

Сбилженные артефакты будут находиться в директориии `dist/`. Флаг `--prod` используется для продакщен-билда.

Запуск проекта
`npm run start`

Запуск линтера
`npm run lint`

## Запуск в докере:
1. В основной папке приложения собираем и запускаем контейнер (первый раз может занять несколько минут): `docker-compose up --build`
   (будет запущен с API стабильной версии (в мастере))
2. Можно смотреть по адресу: `http://tariffcard-spa-local.brokerage.iy/`

### Перед первым запуском:
1. Логинимся в гитлабе: `docker login registry.gitlab.com -u <gitlab user_name> -p <personal_access_token_read_registry>` (Токен: https://gitlab.com/-/profile/personal_access_tokens)
2. Создаем на диске C в корне dev-network: `docker network create dev-network`
3. Любым известным способом создаем системную переменную среды с именем `ENABLE_POLLING` и значением `enabled`

### Для запуска с другой версией API:
   1. Предварительно надо создать мердж реквест для нужной ветки API и дождаться завершения первого шага пайплайна (build feature)
   2. В файле `docker-compose.yaml` для нужного API указать версию не stable, а по номеру ветки (например, `B-2438`).
   3. Если уже был запущен контейнер, сначала остановить
   4. После этого в основной папке приложения подтягиваем себе новую версию образов: `docker-compose pull` 
   5. И запускаем контейнер: `docker-compose up --build`
